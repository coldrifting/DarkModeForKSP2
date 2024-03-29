using BepInEx;
using DarkMode.Patches;
using HarmonyLib;
using JetBrains.Annotations;
using SpaceWarp;
using SpaceWarp.API.Mods;

namespace DarkMode;

[BepInPlugin(ModGuid, ModName, ModVer)]
[BepInDependency(SpaceWarpPlugin.ModGuid, SpaceWarpPlugin.ModVer)]
public class DarkMode : BaseSpaceWarpPlugin
{
    [PublicAPI] public const string ModGuid = MyPluginInfo.PLUGIN_GUID;
    [PublicAPI] public const string ModName = MyPluginInfo.PLUGIN_NAME;
    [PublicAPI] public const string ModVer = MyPluginInfo.PLUGIN_VERSION;
    
    public override void OnInitialized()
    {
        base.OnInitialized();

        Harmony harmony = new Harmony(ModGuid);
        harmony.PatchAll();

        SpaceWarp.API.Game.Messages.StateChanges.KerbalSpaceCenterStateEntered += _ => SpaceCenter.Apply();
        SpaceWarp.API.Game.Messages.StateChanges.KerbalSpaceCenterStateEntered += _ => Launchpad.Apply();
        SpaceWarp.API.Game.Messages.StateChanges.TrainingCenterEntered += _ => Training.Apply();
        SpaceWarp.API.Game.Messages.StateChanges.MissionControlEntered += _ => Missions.Apply();
    }
}
