using System.Collections.Generic;
using System.Linq;
using DragonGentian.DragonGentianCode.Cards;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Characters;

namespace DragonGentian.DragonGentianCode.Patches;

/// <summary>
/// Adds DragonGentianCardInstance to Regent's starting deck for testing.
/// Remove this patch for production release.
/// </summary>
[HarmonyPatch(typeof(Regent), nameof(Regent.StartingDeck), MethodType.Getter)]
public static class StarterDeckPatch
{
    [HarmonyPostfix]
    public static void Postfix(ref IEnumerable<CardModel> __result)
    {
        try
        {
            var card = ModelDb.Card<DragonGentianCardInstance>();
            var list = __result.ToList();
            list.Add(card);
            __result = list;

            MainFile.Logger.Info("Added DragonGentian to Regent starting deck. Total cards: " + list.Count);
        }
        catch (System.Exception ex)
        {
            MainFile.Logger.Warn("Failed to add DragonGentian to starting deck: " + ex.Message);
        }
    }
}
