using System.Reflection;
using DG.Tweening;
using KSP.Game;
using KSP.UI;
using UnityEngine;
using UnityEngine.UI;

namespace DarkMode.Patches;

public static class Launchpad
{
    private static readonly Dictionary<string, Color> LookupColors = new()
    {
        { "BG-panel", Colors.BackgroundColor },
        { "BG-panel/Border", Colors.BorderColor },
        { "GRP-Body/BG-inset", Colors.BackgroundColorInset },
        { "GRP-Header-Facility/Sorting/HideStock/BG-Fill", Colors.BackgroundColorInset },
        { "GRP-Header-Facility/Sorting/VehicleFilter/BG-Fill", Colors.BackgroundColorInset },
        { "GRP-Header-Facility/Sorting/VehicleFilter/DropdownFilter/Text (TMP)", Colors.ForegroundColor },
        { "GRP-Header-Facility/Sorting/HideStock/BTN-Toggle-Checkmark/Label", Colors.ForegroundColor },
        { "GRP-Header-Facility/Main Row/TXT-Title", Colors.ForegroundColor },
        { "GRP-Header-Facility/Main Row/TXT-Title/TILE-PixelBG", Colors.PixelFontBgColor },
        { "GRP-Header-Facility/Main Row/BG-FacilityFlag", Colors.KSCIconColor },
        { "GRP-Body/VehicleInformation/Image", Colors.BackgroundColor },
        { "GRP-Body/VehicleInformation/Top/EditVehicleContent/VehicleName", Colors.ForegroundColor },
        { "GRP-Body/VehicleInformation/Top/EditVehicleContent/CurrentVesselLaunchpad", Colors.ForegroundColor },
        { "GRP-Body/VehicleInformation/Top/EditVehicleContent/VehicleReport/VehicleOverview/VehicleDescription", Colors.ForegroundColor },
        { "GRP-Body/SavedVehiclesList/ELE-ScrollView-Facility/Scrollbar Vertical/Sliding Area/Handle/Border", Colors.BackgroundColor },
        { "GRP-Body/SavedVehiclesList/ELE-ScrollView-Facility/Scrollbar Vertical/Sliding Area/Handle/Border/Grip", Colors.BackgroundColor },
        { "Button Container/DIV-Container-FillEmpty/Tile-Diag-004", Colors.BackgroundColorInset },
        { "Button Container/Bottom/LaunchVehicle/Content", new Color(0.7f, 0.7f, 0.7f)},
        { "Button Container/Bottom/EditVehicleButton/Content", new Color(0.7f, 0.7f, 0.7f)},
        { "Button Container/Bottom/RecoverVehicle/Content", new Color(0.7f, 0.7f, 0.7f)},
    };
    
    private static readonly Dictionary<string, Color> ContentLookup = new()
    {
        { "Content/Section/VehicleName", Colors.ForegroundColor },
        { "Content/Section/Div", Colors.ForegroundColorDim },
        { "Content/Section/Bottom/Parts/Label", Colors.ForegroundColor },
        { "Content/Section/Bottom/Parts/Label/Image", Colors.ForegroundColor },
        { "Content/Section/Bottom/Parts/Information", Colors.ForegroundColorDim },
        { "Content/Section/Bottom/Mass/Label", Colors.ForegroundColor },
        { "Content/Section/Bottom/Mass/Label/Image", Colors.ForegroundColor },
        { "Content/Section/Bottom/Mass/Information", Colors.ForegroundColorDim },
    };

    private static readonly Dictionary<string, (ColorBlock, ColorBlock)> ContentToggleLookup = new()
    {
        { "BG-Stroke", (Colors.ContentColorsOutlineOff, Colors.ContentColorsOutlineActive) },
        { "BG-Fill", (Colors.ContentColorsBG, Colors.ContentColorsBG) }
    };
    
    private static Transform? LaunchpadDialogWindow => GameManager.Instance.Game.LaunchpadDialog is not null 
        ? GameManager.Instance.Game.LaunchpadDialog.transform.Find("Window-FacilityMenu")
        : null;

    private static Transform? LaunchpadDialogDropdown => LaunchpadDialogWindow is not null
        ? LaunchpadDialogWindow.Find("GRP-Header-Facility/Sorting/VehicleFilter/DropdownFilter")
        : null;
    
    public static void Apply()
    {
        ColorSetters.SetColors(LaunchpadDialogWindow, LookupColors);
        FixLaunchpadDropdown(LaunchpadDialogDropdown);
    }

    public static void ApplyDynamic(LaunchpadDialogFileEntry fileEntry)
    {
        ColorSetters.SetToggleEntry(fileEntry.transform, ContentToggleLookup);
        ColorSetters.SetColors(fileEntry.transform, ContentLookup);
    }

    private static void FixLaunchpadDropdown(Transform? dropdownTransform)
    {
        if (dropdownTransform is null)
        {
            return;
        }
        
        DropdownExtendedEventsVisualizer? drop = dropdownTransform.GetComponent<DropdownExtendedEventsVisualizer>();
        FieldInfo? tweenAnimationsField = typeof(DropdownExtendedEventsVisualizer).GetField("tweenAnimations", BindingFlags.Instance | BindingFlags.NonPublic);
        if (tweenAnimationsField is not null)
        {
            DOTweenAnimation[] tweens = (DOTweenAnimation[])tweenAnimationsField.GetValue(drop);
            if (tweens is not null)
            {
                foreach (var tween in tweens)
                {
                    tween.endValueColor = Colors.ForegroundColor;
                    tween.RecreateTween();
                }
            }
        }
    }
}