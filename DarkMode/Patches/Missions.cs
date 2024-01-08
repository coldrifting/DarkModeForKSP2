using KSP.Game;
using KSP.Game.Missions;
using UnityEngine;
using UnityEngine.UI;

namespace DarkMode.Patches;

public static class Missions
{
    private static readonly Dictionary<string, Color> LookupColors = new()
    {
        // Main Window(s)
        { "BG-panel", Colors.BackgroundColor },
        { "BG-panel/Border", Colors.BorderColor },
        { "BG-panel/Depth", Color.black },
        { "BG-panel/Depth (1)", Color.black },
        { "GRP-Body/BG-inset", Colors.BackgroundColorInset },
        { "GRP-Body/BG-inset/BG-insetBorder", Colors.BorderColorInset },
        { "GRP-Header-Facility/Main Row/TXT-Title", Colors.ForegroundColor },
        { "GRP-Header-Facility/Main Row/TXT-Title/TXT-PixelBG", Colors.PixelFontBgColor },
        { "GRP-Header-Facility/Main Row/BG-FacilityFlag", Colors.KSCIconColor },
        { "GRP-Panel-Help/GRP-TrackMission/TXT-TrackMission", Colors.ForegroundColor },
        { "GRP-Panel-Help/GRP-TrackMission/ICO-MiddleClick", Colors.ForegroundColor },
        
        // Popup Window
        { "ELE-ScrollView/BG-inset", Colors.BackgroundColorInset },
        { "ELE-ScrollView/BG-inset/BG-insetBorder", Colors.BorderColorInset },
        { "ELE-ScrollView/Viewport/Content/GRP-Body/GRP-MissionDescription/GRP-MissionDescription-Header/TXT-H2-Standard", Colors.ForegroundColor},
        { "ELE-ScrollView/Viewport/Content/GRP-Body/GRP-MissionDescription/TXT-Body", Colors.ForegroundColor },
        { "ELE-ScrollView/Viewport/Content/GRP-Body/GRP-MissionGranter/TGL-Description/TXT-Name", Colors.ForegroundColor },
        { "ELE-ScrollView/Viewport/Content/GRP-Body/GRP-MissionGranter/TGL-Description/GRP-Texts/TXT-Description", Colors.ForegroundColor },
        
        { "ELE-ScrollView/Viewport/Content/GRP-Body/GRP-MissionObjective/Stat-Value-Pair-Callout/BG-panel", Colors.MissionOverviewBG },
        { "ELE-ScrollView/Viewport/Content/GRP-Body/GRP-MissionObjective/Stat-Value-Pair-Callout/BG-panel/Border", Colors.MissionOverviewStripe },
        { "ELE-ScrollView/Viewport/Content/GRP-Body/GRP-MissionObjective/Stat-Value-Pair-Callout/TXT-Header", Colors.MissionOverviewFG },
        { "ELE-ScrollView/Viewport/Content/GRP-Body/GRP-MissionObjective/Stat-Value-Pair-Callout/GRP-MissionRewards/Stat-Value-Pair-Callout/BG-panel", Colors.MissionRewardBG },
        { "ELE-ScrollView/Viewport/Content/GRP-Body/GRP-MissionObjective/Stat-Value-Pair-Callout/GRP-MissionRewards/Stat-Value-Pair-Callout/BG-panel/Border", Colors.MissionRewardStripe },
        { "ELE-ScrollView/Viewport/Content/GRP-Body/GRP-MissionObjective/Stat-Value-Pair-Callout/GRP-MissionRewards/Stat-Value-Pair-Callout/TXT-Header", Colors.MissionRewardFG },
        
        { "GRP-MissionButtons/BTN-TrackMission/BG-Fill", Colors.MissionButtonGreenBG },
        { "GRP-MissionButtons/BTN-MissionBriefing/BG-Fill", Colors.MissionButtonGreenBG },
        { "GRP-MissionButtons/BTN-MissionDebrief/BG-Fill", Colors.MissionButtonGreenBG },
        { "GRP-MissionButtons/BTN-MissionSubmit/BG-Fill", Colors.MissionButtonBlueBG },
        
        { "GRP-MissionButtons/BTN-TrackMission/Content/Label", Colors.ForegroundColor },
        { "GRP-MissionButtons/BTN-MissionBriefing/Content/Label", Colors.ForegroundColor },
        { "GRP-MissionButtons/BTN-MissionDebrief/Content/Label", Colors.ForegroundColor },
        { "GRP-MissionButtons/BTN-MissionSubmit/Content/Label", Colors.ForegroundColor },
        { "GRP-MissionButtons/BTN-MissionSubmit/Content/Image", Colors.MissionRewardFG },
        
        // Dialog window
        { "Root/BG-panel", Colors.BackgroundColor },
        { "Root/BG-panel/Border", Colors.BorderColor },
        { "Root/GRP-Header-App/Main Row/App Icon", Colors.ForegroundColor },
        { "Root/GRP-Header-App/Main Row/ELE-Dashed-Line", Colors.ForegroundColor },
        { "Root/GRP-Header-App/Main Row/ELE-Dashed-Line (1)", Colors.ForegroundColor },
        { "Root/GRP-Header-App/Main Row/TXT-Title", Colors.ForegroundColor },
        { "Root/GRP-Header-App/Main Row/BTN-Close/Icon", Colors.ForegroundColor },
        { "Root/GRP-Body/BG-Mask (1)", Colors.BorderColorInset },
        { "Root/GRP-Body/BG-Mask/ELE-Masked-Pattern", Colors.BorderColorInset },
        { "Root/GRP-Body/GRP-FooterButtons/BTN-Toggle-Checkmark/Label", Colors.ForegroundColor },
        { "Root/GRP-Body/GRP-FooterButtons/BTN-Toggle-Checkmark/Background", Colors.ForegroundColor },
        { "Root/GRP-Body/MASK-NPCImage/BG-Frame", Colors.BackgroundColorInset },
        { "Root/GRP-Body/GRP-FooterButtons/BTN-Bracket/BG-Fill", Colors.MissionButtonGreenBG },
        { "Root/GRP-Body/GRP-FooterButtons/BTN-Bracket/Content/Label", Colors.ForegroundColor },
        
        // Mission Complete Window
        {"GRP-Body/GRP-VideoPlayer/BG-inset", Colors.BackgroundColorInset },
        {"GRP-Body/GRP-VideoPlayer/BG-inset/BG-insetBorder", Colors.BorderColorInset },
        {"GRP-Body/GRP-VideoPlayer/TXT-Congrats", Colors.ForegroundColorDim },
        {"GRP-Body/GRP-VideoPlayer/Main Row/TXT-Title", Colors.ForegroundColor },
        {"GRP-Body/GRP-VideoPlayer/Main Row/TXT-Title/TILE-PixelBG", Colors.PixelFontBgColor },
        {"GRP-Body/GRP-VideoPlayer/MASK-Video/BG-insetBorder", Colors.BackgroundColor },
        {"GRP-Body/GRP-VideoPlayer/GRP-MissionObjective/GRP-MissionObjective-Header/TXT-H2-Standard", Colors.ForegroundColor},
        {"GRP-Body/GRP-VideoPlayer/GRP-MissionObjective/Content/MissionTriumph-ListItem(Clone)/GRP-MissionStatus/ELE-Brackets", Colors.ForegroundColor},
        {"GRP-Body/GRP-VideoPlayer/GRP-MissionObjective/Content/MissionTriumph-ListItem(Clone)/TXT-Label", Colors.ForegroundColor},
        
        {"GRP-Footer/GRP-Thanks-Button/POSition-Divider/DIV-Ascii-2x (1)", Colors.BorderColorInset },
        {"GRP-Footer/GRP-Thanks-Button/BG-Mask/ELE-Masked-Pattern", Colors.BorderColorInset },
        {"GRP-Footer/GRP-Thanks-Button/BG-Mask (1)/ELE-Masked-Pattern", Colors.BorderColorInset },
        
        {"GRP-Footer/GRP-Thanks-Button/BTN-ThanksScience/BG-Fill", Colors.MissionButtonGreenBG },
        {"GRP-Footer/GRP-Thanks-Button/BTN-ThanksScience/Content/Label", Colors.ForegroundColor },
        {"GRP-Footer/GRP-Thanks-Button/BTN-ThanksScience/Content/Image", Colors.ForegroundColor },
        
        {"GRP-Footer/Reward-Callout/BG-panel", Colors.MissionRewardBG },
        {"GRP-Footer/Reward-Callout/BG-panel/ELE-Bar", Colors.MissionRewardStripe },
        {"GRP-Footer/Reward-Callout/TXT-Header", Colors.MissionRewardFG },
    };
    
    private static readonly Dictionary<string, ColorBlock> LookupButtons = new()
    {
        { "BG-ButtonState", Colors.ButtonBGRed },
    };
    
    private static readonly Dictionary<string, (ColorBlock, ColorBlock)> LookupToggles = new()
    {
        { "BG-Fill", (Colors.ButtonBG, Colors.ButtonBG) }, 
    };

    private static readonly Dictionary<string, Color> LookupEnumColors = new()
    {
        { "Default", Colors.ForegroundColor },
        { "New", Colors.ForegroundColor },
        { "Completed", Colors.ForegroundColor },
        { "Selected", Colors.ForegroundColor },
        { "Submitted", Colors.ForegroundColorDim },
        { "Tracked", Colors.ForegroundColor },
    };

    private static Transform? MissionMenu => GameManager.Instance.Game.MissionControlManager?.MissionControlMenuController.transform;
    private static Transform? MissionMainWindow => MissionMenu is not null ? MissionMenu.Find("MissionLog-FacilityMenu(Clone)/Root/Window-FacilityMenu") : null;
    private static Transform? MissionMainWindowContent => MissionMainWindow is not null ? MissionMainWindow.Find("GRP-Body/ELE-ScrollView/Viewport/Content") : null;
    private static Transform? MissionPopupWindow => MissionMenu is not null ? MissionMenu.Find("Window-Mission-Popover(Clone)/Root/Window-Popover") : null;

    public static void Apply()
    {
        // Main Colors
        ColorSetters.SetColors(MissionMainWindow, LookupColors);
        
        if (MissionMainWindowContent is not null)
        {
            foreach (Transform section in MissionMainWindowContent)
            {
                ColorSetters.SetToggleEntry(section.Find("TGL-Header"), LookupToggles);
                ColorSetters.SetColor(section, "TGL-Header/TXT-H2-Standard" as string, Colors.SelectColorHighlight);
            }
        }
    }

    public static void ApplyAfterLoad()
    {
        // Main Colors
        ColorSetters.SetColors(MissionMainWindow, LookupColors);
        
        if (MissionMainWindowContent is null)
        {
            return;
        }
        
        foreach (Transform section in MissionMainWindowContent)
        {
            if (section.Find("Content") is not { } sectionContent)
            {
                continue;
            }

            foreach (Transform missionButton in sectionContent)
            {
                ColorSetters.SetButtonEntry(missionButton, LookupButtons);
                ColorSetters.SetGraphicEntry(missionButton, LookupEnumColors);
                Utils.Refresh(missionButton.gameObject);
            }
        }
    }

    public static void ApplyPopupWindow()
    {
        ColorSetters.SetColors(MissionPopupWindow, LookupColors);
        ColorSetters.SetColor(MissionPopupWindow, "ELE-ScrollView/Viewport/Content/GRP-Body/GRP-MissionObjective/Stat-Value-Pair-Callout" +
                                                  "/GRP-MissionRewards/Stat-Value-Pair-Callout/Content/Mission-RewardItem(Clone)/ICO-Reward" as string, 
            Colors.MissionRewardFG);
    }

    public static void ApplyCharacterWindow(Transform charDialogWindow)
    {
        ColorSetters.SetColors(charDialogWindow, LookupColors);
        
        Transform? dialogContainer = charDialogWindow.transform.Find("Root/GRP-Body/ELE-ScrollView/Viewport/Content");
        ColorSetters.SetPadding(dialogContainer, top: 2);
    }

    public static void ApplyCharacterWindowNewDialog(CharacterDialogWindow charDialogWindow)
    {
        Transform? dialogContainer = charDialogWindow.transform.Find("Root/GRP-Body/ELE-ScrollView/Viewport/Content");

        foreach (Transform dialog in dialogContainer)
        {
            ColorSetters.SetColor(dialog, (string)"BG-DialogueFrame", Colors.BackgroundColorInset);
            ColorSetters.SetColor(dialog, (string)"BG-DialogueFrame/BG-DialogueBorder", Colors.BorderColorInset);
        }
    }

    public static void ApplyMissionCompleteWindow(CanvasGroup instanceCanvasGroup)
    {
        ColorSetters.SetColors(instanceCanvasGroup.transform, LookupColors, "Root/Window-Triumph");
    }
}