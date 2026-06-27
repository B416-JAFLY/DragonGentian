using System.Collections.Generic;
using BaseLib.Abstracts;
using BaseLib.Utils;
using DragonGentian.DragonGentianCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace DragonGentian.DragonGentianCode.Cards;

[Pool(typeof(RegentCardPool))]
public sealed class DragonGentianCardInstance : CustomCardModel
{
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

    public override List<(string, string)> Localization =>
        LocManager.Instance.Language switch
        {
            "zhs" => new CardLoc("龙胆", "你可以把辉星当作能量，能量当作辉星来使用。"),
            _ => new CardLoc("Dragon Gentian", "You can spend Starbright as Energy, and Energy as Starbright.")
        };
}
