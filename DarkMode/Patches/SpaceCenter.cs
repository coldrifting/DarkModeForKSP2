using KSP.Game;
using KSP.UI;
using UnityEngine;
using UnityEngine.UI;

namespace DarkMode.Patches;

public static class SpaceCenter
{
    private static readonly Dictionary<string, Color> LookupColors = new()
    {
        { "BG-panel", Colors.BackgroundColor },
        { "BG-panel/Border", Colors.BorderColor },
        { "BG-panel/Depth", Color.black },
        { "BG-panel/Depth (1)", Color.black },
        { "GRP-Body/BG-inset", Colors.BackgroundColorInset },
        { "GRP-Header-Facility/Main Row/TXT-Title", Colors.ForegroundColor },
        { "GRP-Header-Facility/Main Row/TXT-Title/TILE-PixelBG", Colors.PixelFontBgColor },
        { "GRP-Header-Facility/Main Row/BG-FacilityFlag", Colors.KSCIconColor },
        { "GRP-Body/Content/Menu/LaunchLocationsFlyoutTarget/BG-panel", Colors.BackgroundColor },
        { "GRP-Body/Content/Menu/LaunchLocationsFlyoutTarget/BG-panel/BG-inset", Colors.BackgroundColorInset },
        { "GRP-Body/Content/Menu/LaunchLocationsFlyoutTarget/BG-panel/Border", Colors.BorderColor }
    };

    private static readonly Dictionary<string, Color> LookupColorsTooltip = new()
    {
        { "Image/TooltipBG/BuildingDesc", Colors.ForegroundColor },
        { "Image/TooltipBG/Depth", Color.black },
    };
    
    private static readonly Dictionary<string, Color> LookupColorsTooltipPlus = new()
    {
        { "Image/TooltipBG", Colors.TooltipColor },
        { "Image/TooltipBG/WindowHeader", Colors.TooltipHeaderColor },
    };
    
    private static readonly Dictionary<string, ColorBlock> LookupButtons = new()
    {
        { "BG", Colors.ButtonBGRed },
        { "TXT", Colors.ButtonFG },
        { "ICO", Colors.ButtonFG }
    };
    
    private static readonly Dictionary<string, (ColorBlock, ColorBlock)> LookupToggles = new()
    {
        { "BG", (Colors.ToggleOffBG, Colors.ToggleOnBG) },
        { "TXT", (Colors.ToggleFG, Colors.ToggleFG) },
        { "ICO", (Colors.ToggleFG, Colors.ToggleFG) }
    };
    
    private static Transform? SpaceCenterMenu => GameManager.Instance.Game.UI.KSCMenu is not null
        ? GameManager.Instance.Game.UI.KSCMenu.transform.Find("Panel/Window-FacilityMenu")
        : null;
    
    public static void Apply()
    {
        if (GameObject.Find(
                "/GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCTargetableBuildingLine/OffScreenWaypointIndicator")
            is { } waypointCircle)
        {
            waypointCircle.SetActive(false);
        }
        
        // Set basic colors
        ColorSetters.SetColors(SpaceCenterMenu, LookupColors);

        List<ButtonExtendedVisualizer> buttons = new();
        
        buttons.AddRange(Utils.FindComponents<ButtonExtendedVisualizer>("GRP-Body/Content/Menu", SpaceCenterMenu));
        buttons.AddRange(Utils.FindComponents<ButtonExtendedVisualizer>("GRP-Body/Content/Menu/LaunchLocationsFlyoutTarget", SpaceCenterMenu));
        
        foreach (var b in buttons)
        {
            ColorSetters.SetButtonEntry(b.transform, LookupButtons);
            ColorSetters.AddButtonEntry(b, "TXT", Colors.ButtonFG);
        }

        if (Utils.FindComponent<ToggleExtendedEventsVisualizer>("GRP-Body/Content/Menu/LaunchLocationFlyoutHeaderToggle", SpaceCenterMenu) is { } toggle)
        {
            ColorSetters.SetToggleEntry(toggle.transform, LookupToggles);
        }
    }

    public static void ApplyTooltip(Transform tooltip, bool applyPlus = false)
    {
        ColorSetters.SetColors(tooltip, LookupColorsTooltip);

        if (applyPlus)
        {
            ColorSetters.SetColors(tooltip, LookupColorsTooltipPlus);
        }
    }
}