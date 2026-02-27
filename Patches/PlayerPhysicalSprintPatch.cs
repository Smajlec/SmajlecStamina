using System.Reflection;
using SPT.Reflection.Patching;

namespace SmajlecStamina.Patches;

internal class PlayerPhysicalSprintPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return typeof(PlayerPhysicalClass).GetMethod(nameof(PlayerPhysicalClass.Sprint), BindingFlags.Public | BindingFlags.Instance);
    }

    // Override the method, CanSprint might be inlined and the result cannot be patched (or I don't know how to do it)
    [PatchPrefix]
    private static bool PatchPrefix(ref PlayerPhysicalClass __instance, bool target)
    {
        __instance.Consumptions[PlayerPhysicalClass.EConsumptionType.Sprint].SetActive(__instance, target);
        
        if (!target) return false;
        
        __instance.Stamina.Consume(__instance.Gclass773_0);
        
        return false;
    }
}