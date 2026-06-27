using System;
using System.Linq;
using System.Reflection;
using DragonGentian.DragonGentianCode.Powers;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;

namespace DragonGentian.DragonGentianCode.Patches;

/// <summary>
/// 龙胆 1:1 互通补丁。
///
/// 策略：不修改卡牌内部状态（避免 SetUntilPlayed 积累和兼容性问题），
/// 而是在三个关键查询方法上 postfix 覆盖返回值：
///   GetWithModifiers      — CanPlay / HasEnoughResourcesFor 查询能量费用
///   GetAmountToSpend      — SpendResources 确定实际扣除的能量
///   GetStarCostWithModifiers — UI / CanPlay / SpendResources 查询辉星费用
///
/// ShouldPayExcessEnergyCostWithStars = false，让游戏走简单的分别比较逻辑。
/// </summary>

internal static class SplitComputer
{
    private static readonly FieldInfo? CardField = typeof(CardEnergyCost)
        .GetField("_card", BindingFlags.NonPublic | BindingFlags.Instance);

    public static bool TryComputeSplit(
        object ownerObj,
        out int newEnergy,
        out int newStar)
    {
        newEnergy = 0;
        newStar = 0;

        CardModel card;
        if (ownerObj is CardModel c)
            card = c;
        else if (ownerObj is CardEnergyCost cec && CardField != null)
            card = (CardModel)CardField.GetValue(cec)!;
        else
            return false;

        var player = card.Owner;
        if (player == null) return false;
        var creature = player.Creature;
        if (creature == null) return false;
        if (!creature.Powers.Any(p => p is DragonGentianDragonPower)) return false;

        var energyCostObj = card.EnergyCost;
        if (energyCostObj == null) return false;

        var rawEnergy = energyCostObj.Canonical;
        var rawStar = card.BaseStarCost;
        var effStar = Math.Max(0, rawStar);

        var pcs = player.PlayerCombatState;
        if (pcs == null) return false;
        var energy = pcs.Energy;
        var stars = pcs.Stars;

        if (energy >= rawEnergy && stars >= effStar)
        {
            newEnergy = rawEnergy;
            newStar = effStar;
        }
        else if (energy < rawEnergy && stars >= effStar + (rawEnergy - energy))
        {
            newEnergy = energy;
            newStar = effStar + (rawEnergy - energy);
        }
        else if (stars < effStar && energy >= rawEnergy + (effStar - stars))
        {
            newEnergy = rawEnergy + (effStar - stars);
            newStar = stars;
        }
        else
        {
            newEnergy = rawEnergy;
            newStar = effStar;
        }
        return true;
    }
}

[HarmonyPatch(typeof(CardEnergyCost), nameof(CardEnergyCost.GetWithModifiers))]
public static class DragonGentianEnergyCostPatch
{
    [HarmonyPostfix]
    public static void Postfix(CardEnergyCost __instance, ref int __result)
    {
        try
        {
            if (SplitComputer.TryComputeSplit(__instance, out var newEnergy, out _))
            {
                __result = newEnergy;
            }
        }
        catch (Exception ex)
        {
            MainFile.Logger.Warn($"[DragonGentian] GetWithModifiers patch error: {ex}");
        }
    }
}

[HarmonyPatch(typeof(CardEnergyCost), nameof(CardEnergyCost.GetAmountToSpend))]
public static class DragonGentianGetAmountToSpendPatch
{
    [HarmonyPostfix]
    public static void Postfix(CardEnergyCost __instance, ref int __result)
    {
        try
        {
            if (SplitComputer.TryComputeSplit(__instance, out var newEnergy, out _))
            {
                __result = newEnergy;
            }
        }
        catch (Exception ex)
        {
            MainFile.Logger.Warn($"[DragonGentian] GetAmountToSpend patch error: {ex}");
        }
    }
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.GetStarCostWithModifiers))]
public static class DragonGentianGetStarCostWithModifiersPatch
{
    [HarmonyPostfix]
    public static void Postfix(CardModel __instance, ref int __result)
    {
        try
        {
            if (SplitComputer.TryComputeSplit(__instance, out _, out var newStar))
            {
                __result = newStar;
            }
        }
        catch (Exception ex)
        {
            MainFile.Logger.Warn($"[DragonGentian] GetStarCostWithModifiers patch error: {ex}");
        }
    }
}
