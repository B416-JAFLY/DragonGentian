using System.Collections.Generic;
using BaseLib.Abstracts;
using BaseLib.Utils;
using DragonGentian.DragonGentianCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace DragonGentian.DragonGentianCode.Cards;

[Pool(typeof(RegentCardPool))]
public sealed class DragonGentianCardInstance : CustomCardModel
{
    public override string PortraitPath =>
        "res://DragonGentian/images/card_portraits/dragon_gentian_card_instance.png";

    public DragonGentianCardInstance()
        : base(2, CardType.Power, CardRarity.Uncommon, TargetType.None)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await PowerCmd.Apply<DragonGentianDragonPower>(
            new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            1,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
        base.OnUpgrade();
        EnergyCost.UpgradeBy(-1);
        AddKeyword(CardKeyword.Innate);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            foreach (var tip in base.ExtraHoverTips)
                yield return tip;
            yield return HoverTipFactory.ForEnergy(this);
        }
    }

    public override List<(string, string)> Localization =>
        LocManager.Instance.Language switch
        {
            "zhs" => new CardLoc("龙胆", "你可以把{singleStarIcon}当作{energyPrefix:energyIcons(1)}，{energyPrefix:energyIcons(1)}当作{singleStarIcon}来使用。"),
            _ => new CardLoc("Dragon Gentian", "You can spend {singleStarIcon} as {energyPrefix:energyIcons(1)}, and {energyPrefix:energyIcons(1)} as {singleStarIcon}.")
        };
}
