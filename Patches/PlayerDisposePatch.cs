using System.Reflection;
using EFT;
using SPT.Reflection.Patching;

namespace SmajlecStamina.Patches;

public class PlayerDisposePatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return typeof(Player).GetMethod(nameof(Player.Dispose), BindingFlags.Public | BindingFlags.Instance);
    }

    [PatchPrefix]
    private static bool PatchPrefix(ref Player __instance)
    {
        if (!Plugin.ThresholdPassHandlers.TryGetValue(__instance, out var handler))
            return true;

        __instance.Physical.Stamina.OnThresholdPass -= handler;
        Plugin.ThresholdPassHandlers.Remove(__instance);

        return true;
    }
}