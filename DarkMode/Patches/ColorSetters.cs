using KSP.UI;
using KSP.UI.Binding.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DarkMode.Patches;

public static class ColorSetters
{
    // -- Basic Color and size setting -- //
    public static void SetPadding(Transform? t, int? top = null, int? bottom = null, int? left = null, int? right = null)
    {
        if (t is not null && t.GetComponent<VerticalLayoutGroup>() is { } lg)
        {
            lg.padding.top = top ?? lg.padding.top;
            lg.padding.bottom = bottom ?? lg.padding.bottom;
            lg.padding.left = left ?? lg.padding.left;
            lg.padding.right = right ?? lg.padding.right;
        }
    }
    
    public static void SetColor(Transform? t, string path, Color color)
    {
        if (t is not null && t.Find(path) is { } child)
        {
            if (child.GetComponent<Image>() is { } image)
            {
                image.color = color;
            }
            else if (child.GetComponent<TextMeshProUGUI>() is { } text)
            {
                text.color = color;
            }
        }
    }
    
    public static void SetColors(Transform? parent, Dictionary<string, Color> lookups, string parentPath = "")
    {
        if (parent is null)
        {
            return;
        }

        Transform? t = parentPath is "" ? parent : parent.Find(parentPath);
        foreach (var (path, color) in lookups)
        {
            SetColor(t, path, color);
        }
    }

    // -- Extended button and toggle visualizers -- //
    public static void SetButtonEntry(Transform? buttonTransform, IReadOnlyDictionary<string, ColorBlock> colors)
    {
        if (buttonTransform is null || buttonTransform.GetComponent<ButtonExtendedVisualizer>() is not { } button)
        {
            return;
        }
        
        for (int i = 0; i < button.transitionVisuals.Length; i++)
        {
            if (button.transitionVisuals[i].targetGraphics.First() is not { } target)
            {
                continue;
            }

            if (TryGetValuePrefixed(colors, target.name, out ColorBlock color))
            {
                target.color = new Color(1, 1, 1);
                TransitionVisualData.ApplyColorBlock(ref button.transitionVisuals[i].transitionData, color);
            }
        }
        
        button.RefreshVisuals(true);
    }

    public static void SetToggleEntry(Transform? toggleTransform, IReadOnlyDictionary<string, (ColorBlock, ColorBlock)> colors)
    {
        if (toggleTransform is null || toggleTransform.GetComponent<ToggleExtendedEventsVisualizer>() is not { } toggle)
        {
            return;
        }

        for (int i = 0; i < toggle.TransitionVisualsToggle.Length; i++)
        {
            if (toggle.TransitionVisualsToggle[i].targetGraphics.First() is not { } target)
            {
                continue;
            }

            if (TryGetValuePrefixed(colors, target.name, out (ColorBlock, ColorBlock) color))
            {
                target.color = new Color(1, 1, 1);
                TransitionVisualData.ApplyColorBlock(ref toggle.TransitionVisualsToggle[i].transitionDataOff, color.Item1);
                TransitionVisualData.ApplyColorBlock(ref toggle.TransitionVisualsToggle[i].transitionDataOn, color.Item2);
            }
        }
        
        toggle.RefreshVisuals(true);
    }

    public static void AddButtonEntry(ButtonExtendedVisualizer button, string target, ColorBlock colors)
    {
        Graphic? graphic = GetGraphic(button.transform, target);
        if (graphic is null)
        {
            return;
        }
        
        List<TransitionVisualSingle> l = button.transitionVisuals.ToList();
        foreach (var v in l)
        {
            if (v.targetGraphics.Contains(graphic))
            {
                return;
            }
        }

        var tvs = new TransitionVisualSingle();
        tvs.targetGraphics = new[] { graphic };
        tvs.transitionData = TransitionVisualData.Default;
        TransitionVisualData.ApplyColorBlock(ref tvs.transitionData, colors);
        l.Add(tvs);
        
        graphic.color = colors.normalColor;
        
        button.transitionVisuals = l.ToArray();
        button.RefreshVisuals(true);
    }
    
    
    // -- Helper functions -- //
    private static Graphic? GetGraphic(Transform t, string target)
    {
        if (t.FindChildrenIfNameContains(target).FirstOrDefault() is { } targetTransform)
        {
            if (targetTransform.GetComponent<Image>() is { } image)
            {
                return image;
            }

            if (targetTransform.GetComponent<TextMeshProUGUI>() is { } text)
            {
                return text;
            }
        }

        return null;
    }
    
    public static void SetGraphicEntry(Transform? t, Dictionary<string, Color> lookupEnumColors)
    {
        if (t is not null && t.GetComponent<UIValue_ReadEnum_GraphicSet>() is { } gs)
        {
            foreach (var (name, color) in lookupEnumColors)
            {
                gs.valueMap[name] = 
                    new UIValue_ReadEnum_GraphicSet.GraphicEntry()
                    {
                        enumValue = name, 
                        color = color
                    };
            }
        }
    }

    private static bool TryGetValuePrefixed<T>(IReadOnlyDictionary<string, T> dictionary, string key, out T? value)
    {
        if (dictionary.TryGetValue(key, out T outVal))
        {
            value = outVal;
            return true;
        }

        foreach ((string curKey, T curValue) in dictionary)
        {
            if (key.StartsWith(curKey))
            {
                value = curValue;
                return true;
            }
        }

        value = default;
        return false;
    }
}