using System.Reflection;
using SPT.Reflection.Patching;

namespace SmajlecStamina.Patches;

internal class PlayerPhysicalInitPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return typeof(PlayerPhysicalClass).GetMethod(nameof(PlayerPhysicalClass.Init), BindingFlags.Instance | BindingFlags.Public);
    }

    [PatchPostfix]
    private static void PatchPostfix(ref PlayerPhysicalClass __instance)
    {
        __instance.Consumptions[PlayerPhysicalClass.EConsumptionType.Sprint].Requires = PlayerPhysicalClass.EConsumptionTarget.None;
        __instance.Consumptions[PlayerPhysicalClass.EConsumptionType.Sprint].Downtime = Plugin.SprintDowntime.Value;
        
        __instance.Consumptions[PlayerPhysicalClass.EConsumptionType.Jump].StartThreshold = 0f;
        __instance.Consumptions[PlayerPhysicalClass.EConsumptionType.VaultLegs].StartThreshold = 0f;
        __instance.Consumptions[PlayerPhysicalClass.EConsumptionType.ClimbLegs].StartThreshold = 0f;
        __instance.Consumptions[PlayerPhysicalClass.EConsumptionType.VaultHands].StartThreshold = 0f;
        __instance.Consumptions[PlayerPhysicalClass.EConsumptionType.ClimbHands].StartThreshold = 0f;
    }
}