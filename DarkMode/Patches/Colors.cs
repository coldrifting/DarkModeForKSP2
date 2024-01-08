using UnityEngine;
using UnityEngine.UI;

namespace DarkMode.Patches;

public static class Colors
{
    public static Color ForegroundColor = new(0.9f, 0.9f, 0.9f);
    public static Color ForegroundColorDim = new(0.65f, 0.65f, 0.65f);
    
    public static Color BackgroundColor = new(0.1f, 0.12f, 0.15f);
    public static Color BackgroundColorInset = new(0.2f, 0.22f, 0.25f);
    
    public static Color HoverColor = new(0.1f, 0.12f, 0.15f);
    
    public static Color BorderColor = new(0.2f, 0.22f, 0.25f);
    public static Color BorderColorInset = new(0.4f, 0.4f, 0.56f);
    
    public static Color PixelFontBgColor = new(1, 1, 1, 0.005f);

    public static Color TooltipColor = new(BackgroundColor.r, BackgroundColor.g, BackgroundColor.b, 0.95f);
    public static Color TooltipHeaderColor = new(0, 0, 0, 0.5f);
    
    public static readonly Color KSCIconColor = new(0.64f, 0.2f, 0.23f);
    
    public static readonly Color SelectColor = new(0.64f, 0.2f, 0.23f);
    public static readonly Color SelectColorHighlight = Utils.AdjustColor(SelectColor, 1.2f);
    private static readonly Color SelectColorPressed = Utils.AdjustColor(SelectColor, 0.8f);

    public static readonly Color MissionOverviewBG = new Color(0.75f, 0.6f, 0, 0.75f);
    public static readonly Color MissionOverviewStripe = new Color(0.9f, 0.75f, 0);
    public static readonly Color MissionOverviewFG = new Color(1f, 0.85f, 0.2f);
    
    public static readonly Color MissionRewardBG = new Color(0.1f, 0.4f, 0.5f);
    public static readonly Color MissionRewardStripe = new Color(0.25f, 0.6f, 0.75f);
    public static readonly Color MissionRewardFG = new Color(0.45f, 0.85f, 1);

    public static readonly Color MissionButtonBlueBG = new Color(0.1f, 0.4f, 0.5f);
    public static readonly Color MissionButtonGreenBG = new Color(0, 0.35f, 0.15f);
    
    private static readonly Color InfoColor = new(0.95f, 0.75f, 0.25f);
    
    public static readonly Color BlueprintColor = new(0.025f, 0.05f, 0.1f);
    
    // Buttons
    public static ColorBlock ButtonBG = new()
    {
        normalColor = Color.clear,
        highlightedColor = HoverColor,
        pressedColor = ForegroundColor
    };
    
    public static ColorBlock ButtonBGRed = new()
    {
        normalColor = Color.clear,
        highlightedColor = HoverColor,
        pressedColor = SelectColor
    };
        
    public static ColorBlock ButtonFG = new()
    {
        normalColor = ForegroundColor,
        highlightedColor = ForegroundColor,
        pressedColor = HoverColor
    };
        
    public static ColorBlock ButtonFGRed = new()
    {
        normalColor = ForegroundColor,
        highlightedColor = ForegroundColor,
        pressedColor = ForegroundColor
    };
    
    // Toggles
    public static ColorBlock ToggleOffBG = new()
    {
        normalColor = Color.clear,
        highlightedColor = HoverColor,
        pressedColor = SelectColor
    };
    
    public static ColorBlock ToggleOnBG = new()
    {
        normalColor = SelectColor,
        highlightedColor = SelectColorHighlight,
        pressedColor = SelectColorPressed
    };
        
    public static ColorBlock ToggleFG = new()
    {
        normalColor = ForegroundColor,
        highlightedColor = ForegroundColor,
        pressedColor = ForegroundColor
    };
    
    // Other stuff
    public static ColorBlock FGRed = new()
    {
        normalColor = SelectColor,
        highlightedColor = SelectColor,
        pressedColor = ForegroundColor
    };
            
    public static ColorBlock ToggleOffProgressBG = new()
    {
        normalColor = InfoColor,
        highlightedColor = InfoColor,
        pressedColor = Color.clear,
    };
            
    public static ColorBlock ToggleOnProgressBG = new()
    {
        normalColor = Color.clear,
        highlightedColor = Color.clear,
        pressedColor = Color.clear,
    };
    
    public static ColorBlock ToggleOffProgressFG = new()
    {
        normalColor = BackgroundColor,
        highlightedColor = BackgroundColor,
        pressedColor = ForegroundColor
    };
    
    // Outlines
    public static ColorBlock ContentColorsBG = new()
    {
        normalColor = HoverColor,
        highlightedColor = HoverColor,
        pressedColor = HoverColor,
        selectedColor = HoverColor
    };
            
    public static ColorBlock ContentColorsOutlineActive = new()
    {
        normalColor = SelectColor,
        highlightedColor = new Color(SelectColor.r, SelectColor.g, SelectColor.b, 0.2f),
        pressedColor = new Color(SelectColor.r, SelectColor.g, SelectColor.b, 0.8f),
        selectedColor = new Color(SelectColor.r, SelectColor.g, SelectColor.b, 0.4f)
    };
            
    public static ColorBlock ContentColorsOutlineOff = new()
    {
        normalColor = HoverColor,
        highlightedColor = new Color(SelectColor.r, SelectColor.g, SelectColor.b, 0.2f),
        pressedColor = new Color(SelectColor.r, SelectColor.g, SelectColor.b, 0.8f),
        selectedColor = new Color(SelectColor.r, SelectColor.g, SelectColor.b, 0.4f)
    };
}