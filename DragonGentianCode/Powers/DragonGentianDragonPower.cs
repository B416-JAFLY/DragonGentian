using System.Collections.Generic;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
namespace DragonGentian.DragonGentianCode.Powers;

public sealed class DragonGentianDragonPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    /// <summary>
    /// 本地化（无需 .pck，直接在代码中定义）。
    /// </summary>
    public override List<(string, string)> Localization =>
        LocManager.Instance.Language switch
        {
            "zhs" => new PowerLoc("龙胆", "你可以把辉星当作能量，能量当作辉星来使用。", "辉星与能量互通使用。"),
            _ => new PowerLoc("Dragon Gentian", "You can spend Starbright as Energy, and Energy as Starbright.", "Starbright and Energy are interchangeable.")
        };
}
