using KSP.Game;
using UnityEngine;
using UnityEngine.UI;

namespace DarkMode.Patches;

public static class Training
{
    private static readonly Dictionary<string, Color> LookupColors = new()
    {
        { "BG-panel", Colors.BackgroundColor },
        { "BG-panel/Border", Colors.BorderColor },
        { "BG-panel/Depth", Color.black },
        { "BG-panel/Depth (1)", Color.black },
        { "tableOfContents/Scroll View/BG-inset", Colors.BackgroundColorInset },
        { "tableOfContents/Scroll View/BG-inset/BG-insetBorder", Colors.BorderColorInset },
        { "Title/GRP-Header-Facility/Main Row/TXT-Title", Colors.ForegroundColor },
        { "Title/GRP-Header-Facility/Main Row/TXT-Title/TXT-PixelBG", Colors.PixelFontBgColor },
        { "Title/GRP-Header-Facility/Main Row/BG-FacilityFlag", Colors.KSCIconColor },
        { "LessonsContent/BG-inset", Colors.BackgroundColorInset },
        { "LessonsContent/BG-inset/BG-insetBorder", Colors.BorderColorInset },
        { "SelectCourseContent/LCD_Frame/TC_CourseDescription/Description Layout/Main Row/TutorialTitle", Colors.ForegroundColor },
        { "SelectCourseContent/LCD_Frame/TC_CourseDescription/Description Layout/Main Row/TILE-PixelBG", Colors.PixelFontBgColor },
        { "SelectCourseContent/LCD_Frame/TC_CourseDescription/Description Layout/Scroll View/Viewport/Content/GroupDescription", Colors.ForegroundColor }
    };
    
    private static readonly Dictionary<string, ColorBlock> LookupButtons = new()
    {
        { "TC_LessonItem(Clone)", Colors.ButtonBGRed },
        { "IconHolder", Colors.ButtonFGRed },
        { "CompletionMarker", Colors.ButtonFGRed }
    };
    
    private static readonly Dictionary<string, (ColorBlock, ColorBlock)> LookupToggles = new()
    {
        { "KSP2Toggle", (Colors.ToggleOffBG, Colors.ToggleOnBG) }, 
        { "Asterisk", (Colors.FGRed, Colors.ToggleFG) }, 
        { "TutorialName", (Colors.ToggleFG, Colors.ToggleFG) }, 
        { "ELE-Pattern", (Colors.ToggleFG, Colors.ToggleFG) }, 
        { "Progress", (Colors.ToggleOffProgressBG, Colors.ToggleOnProgressBG) }, 
        { "InProgress", (Colors.ToggleOffProgressFG, Colors.ToggleFG) }
    };

    private static Transform? TrainingCenterRoot => GameManager.Instance.Game.TrainingCenterDialog is not null
        ? GameManager.Instance.Game.TrainingCenterDialog.transform
        : null;

    private static Transform? TrainingCenterContent => TrainingCenterRoot is not null
        ? TrainingCenterRoot.Find("TC_Frame/TC_TutorialSelectionWindow/TC_CourseItems")
        : null;
    
    private static Transform? TrainingCenterContentScroll => TrainingCenterContent is not null
        ? TrainingCenterContent.Find("tableOfContents/Scroll View/Viewport/Content")
        : null;
    
    private static Transform? TrainingCenterLesson => TrainingCenterRoot is not null
        ? TrainingCenterRoot.Find("TC_Frame/TC_TutorialSelectionWindow/TC_LessonItem")
        : null;
    
    public static void Apply()
    {
        // Main Colors
        ColorSetters.SetColors(TrainingCenterContent, LookupColors);

        // Main Toggles
        if (TrainingCenterContentScroll is not null)
        {
            foreach (Transform contentTransform in TrainingCenterContentScroll)
            {
                if (contentTransform.Find("KSP2Toggle") is { } toggleTransform)
                {
                    ColorSetters.SetToggleEntry(toggleTransform, LookupToggles);
                }
            }
        }
    }

    public static void ApplyDynamic()
    {
        // Main Colors
        ColorSetters.SetColors(TrainingCenterLesson, LookupColors);
        
        // Main Buttons
        if (TrainingCenterLesson is not null)
        {
            if (TrainingCenterLesson.Find("LessonsContent/tableOfContents/Scroll View/Viewport/Content") is { } buttonContainer)
            {
                ColorSetters.SetPadding(buttonContainer, left: 1, right: 1);
            
                foreach (Transform buttonTransform in buttonContainer)
                {
                    ColorSetters.SetButtonEntry(buttonTransform, LookupButtons);
                }
            }
        }
    }
}