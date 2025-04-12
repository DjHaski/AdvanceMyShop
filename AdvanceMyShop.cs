using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace AdvanceMyShop;

[BepInPlugin("Dj_Haski.AdvanceMyShop", "AdvanceMyShop", "1.0")]
public class AdvanceMyShop : BaseUnityPlugin
{
    internal static AdvanceMyShop Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger => Instance._logger;
    private ManualLogSource _logger => base.Logger;
    internal Harmony? Harmony { get; set; }

    private void Awake()
    {
        Instance = this;

        PluginConfig.Load(Config);
        
        // Prevent the plugin from being deleted
        this.gameObject.transform.parent = null;
        this.gameObject.hideFlags = HideFlags.HideAndDontSave;

        Patch();


        Logger.LogInfo($"v{Info.Metadata.Version} is now injected in the game. Have fun :3");
    }

    internal void Patch()
    {
        Harmony ??= new Harmony(Info.Metadata.GUID);
        Harmony.PatchAll();
    }

    internal void Unpatch()
    {
        Harmony?.UnpatchSelf();
    }
}