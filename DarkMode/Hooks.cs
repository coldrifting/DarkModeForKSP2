using DarkMode.Patches;
using HarmonyLib;
using KSP.Game;
using KSP.Game.Missions;
using KSP.UI;

namespace DarkMode;

// ReSharper disable InconsistentNaming
public static class Hooks
{
    [HarmonyPatch(typeof(KSCBuildingTooltip), nameof(KSCBuildingTooltip.Show))]
    public static class KSCTooltipHook
    {
        [HarmonyPatch]
        public static void Postfix(ref KSCBuildingTooltip __instance)
        {
            __instance._defaultColor = Colors.TooltipColor;
            __instance._defaultHeaderColor = Colors.TooltipHeaderColor;
            
            SpaceCenter.ApplyTooltip(__instance.transform);
        }
    }
    
    [HarmonyPatch(typeof(KSCButtonTooltip), nameof(KSCButtonTooltip.UpdateTooltipData))]
    public static class KSCTooltipHook2
    {
        [HarmonyPatch]
        public static void Postfix(ref KSCButtonTooltip __instance)
        {
            SpaceCenter.ApplyTooltip(__instance.transform, true);
        }
    }
    
    [HarmonyPatch(typeof(LaunchpadDialog), nameof(LaunchpadDialog.AddSaveLoadFileEntry))]
    public static class LaunchpadHook
    {
        [HarmonyPatch]
        public static void Postfix(ref LaunchpadDialog __instance)
        {
            List<LaunchpadDialogFileEntry> l = __instance._vehiclesSaveList;
            if (l is not null && l.Count > 0)
            {
                Launchpad.ApplyDynamic(l.Last());
            }
        }
    }
    
    [HarmonyPatch(typeof(TrainingCenterMenuController), nameof(TrainingCenterMenuController.SetSubMenuVisible))]
    public static class TrainingMenuHook
    {
        [HarmonyPatch]
        public static void Postfix(ref LaunchpadDialog __instance)
        {
            Training.ApplyDynamic();
        }
    }
    
    // -- Mission Control -- //
    [HarmonyPatch(typeof(MissionControlMenuController), nameof(MissionControlMenuController.AddMissionUIElement))]
    public static class MissionControlFinishedAddingMissionsHook
    {
        [HarmonyPatch]
        public static void Postfix()
        {
            Missions.ApplyAfterLoad();
        }
    }
    
    [HarmonyPatch(typeof(MissionPopover), nameof(MissionPopover.ShowWindow))]
    public static class MissionControlHook
    {
        [HarmonyPatch]
        public static void Postfix()
        {
            Missions.ApplyPopupWindow();
        }
    }
    
    [HarmonyPatch(typeof(CharacterDialogWindow), nameof(CharacterDialogWindow.SetVisible))]
    public static class MissionControlDialogWindowHook
    {
        [HarmonyPatch]
        public static void Postfix(ref CharacterDialogWindow __instance)
        {
            Missions.ApplyCharacterWindow(__instance.transform);
        }
    }
    
    [HarmonyPatch(typeof(CharacterDialogWindow), nameof(CharacterDialogWindow.PlayNextDialog))]
    public static class MissionControlDialogEntryHook
    {
        [HarmonyPatch]
        public static void Postfix(ref CharacterDialogWindow __instance)
        {
            Missions.ApplyCharacterWindowNewDialog(__instance);
        }
    }
    
    [HarmonyPatch(typeof(MissionTriumphWindow), nameof(MissionTriumphWindow.ShowWindow))]
    public static class MissionCompleteWindowHook
    {
        [HarmonyPatch]
        public static void Postfix(ref MissionTriumphWindow __instance)
        {
            Missions.ApplyMissionCompleteWindow(__instance._canvasGroup);
        }
    }
    
    [HarmonyPatch(typeof(ObjectAssemblyViewCube), nameof(ObjectAssemblyViewCube.EnterOrthoView))]
    public static class VABBlueprintHook
    {
        [HarmonyPatch]
        public static void Postfix()
        {
            Editor.Apply();
        }
    }
}