using System.Reflection;
using EFT;
using SPT.Reflection.Patching;

namespace SmajlecStamina.Patches;

public class PlayerInitPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return typeof(Player).GetMethod(nameof(Player.Init), BindingFlags.Public | BindingFlags.Instance);
    }

    [PatchPostfix]
    private static void PatchPostfix(ref Player __instance)
    {
        var player = __instance;
        var handler = () =>
        {
            player.MovementContext.UpdateCharacterControllerSpeedLimit();
        };

        player.Physical.Stamina.OnThresholdPass += handler;
        Plugin.ThresholdPassHandlers[player] = handler;
    }
}