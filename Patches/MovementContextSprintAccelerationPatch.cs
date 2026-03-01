using System.Reflection;
using EFT;
using SPT.Reflection.Patching;
using UnityEngine;

namespace SmajlecStamina.Patches;

public class MovementContextSprintAccelerationPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return typeof(MovementContext).GetMethod(nameof(MovementContext.SprintAcceleration));
    }

    [PatchPostfix]
    private static void PatchPostfix(ref MovementContext __instance, Player ____player)
    {
        if (!____player.Physical.Stamina.Exhausted) return;

        __instance.PlayerAnimator_1.SetCharacterSprintSpeed(Mathf.Pow(Plugin.JogSpeed.Value + .145f, 3)); // These values felt good, they may not necessarily be the most accurate
    }
}