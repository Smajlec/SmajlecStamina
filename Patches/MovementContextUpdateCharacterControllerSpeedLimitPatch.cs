using System.Reflection;
using EFT;
using SPT.Reflection.Patching;

namespace SmajlecStamina.Patches;

public class MovementContextUpdateCharacterControllerSpeedLimitPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return typeof(MovementContext).GetMethod(nameof(MovementContext.UpdateCharacterControllerSpeedLimit));
    }
    
    // Override only when sprinting
    [PatchPrefix]
    private static bool PatchPrefix(ref MovementContext __instance, Player ____player)
    {
        if (__instance.CurrentState is GClass2137) return true;
        if (__instance.CurrentState.Name != EPlayerState.Sprint) return true;

        var maxSpeed = ____player.Physical.Exhausted ? Plugin.JogSpeed.Value : 1f;
        
        __instance.SpeedLimiter?.Update(__instance.CurrentState.Name, __instance.SkillManager.Strength.SummaryLevel, maxSpeed);
        
        __instance.SetCharacterMovementSpeed(__instance.RelativeSpeed * __instance.MaxSpeed);
        return false;
    }
}