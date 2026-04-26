using System.Reflection;
using EFT.HealthSystem;
using SPT.Reflection.Patching;

namespace SmajlecStamina.Patches;

public class ActiveHealthControllerAddFatiguePatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return typeof(ActiveHealthController).GetMethod(nameof(ActiveHealthController.AddFatigue));
    }

    [PatchPrefix]
    private static bool PatchPrefix()
    {
        return !Plugin.DisableFatigue.Value;
    }
}