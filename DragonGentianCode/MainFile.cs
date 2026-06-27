using System.Reflection;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;

namespace DragonGentian.DragonGentianCode;

[ModInitializer(nameof(Initialize))]
public static class MainFile
{
    public const string ModId = "DragonGentian";

    public static Logger Logger { get; } = new(ModId, LogType.Generic);

    public static void Initialize()
    {
        Logger.Info("DragonGentian mod initializing...");

        var harmony = new Harmony(ModId);
        harmony.PatchAll(Assembly.GetExecutingAssembly());

        Logger.Info("DragonGentian mod initialized successfully.");
    }
}
