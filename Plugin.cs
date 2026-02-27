using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using EFT;
using SmajlecStamina.Patches;

namespace SmajlecStamina;

[BepInPlugin("com.smajlec.stamina", "SmajlecStamina", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    // Config
    public static ConfigEntry<float> JogSpeed { get; set; }
    public static ConfigEntry<float> SprintDowntime { get; set; }

    // Plugin stuff
    public static ManualLogSource LogSource;

    // Event related
    public static readonly Dictionary<Player, Action> ThresholdPassHandlers = new();

    // Unity Hooks
    private void Awake()
    {
        // Logger
        LogSource = Logger;

        // Patches
        new PlayerPhysicalInitPatch().Enable();
        new PlayerPhysicalSprintPatch().Enable();
        new MovementContextUpdateCharacterControllerSpeedLimitPatch().Enable();
        new PlayerInitPatch().Enable();
        new PlayerDisposePatch().Enable();
    }

    private void Start()
    {
        JogSpeed = Config.Bind("Main", "Jog Speed", .65f, new ConfigDescription("Running speed when stamina is depleted", new AcceptableValueRange<float>(.55f, 1f), new ConfigurationManagerAttributes
        {
            Order = 1000
        }));
        SprintDowntime = Config.Bind("Main", "Sprint Downtime", .1f, new ConfigDescription("Time needed to start regenerating stamina after sprinting", new AcceptableValueRange<float>(.1f, 1f), new ConfigurationManagerAttributes
        {
            Order = 999
        }));
    }
}