using UnityEngine;

namespace DarkMode.Patches;

public static class Editor
{
    private const int ImageSize = 1024;
    
    private static readonly Sprite BlueprintSprite = GenerateBlueprintSprite();
    
    public static void Apply()
    {
        if (Utils.FindComponent<SpriteRenderer>("/blueprintSmall(Clone)") is { } blueprintSmall)
        {
            blueprintSmall.sprite = BlueprintSprite;
            blueprintSmall.tileMode = SpriteTileMode.Adaptive;
        }
        
        if (Utils.FindComponent<SpriteRenderer>("/blueprintLarge(Clone)") is { } blueprintLarge)
        {
            blueprintLarge.sprite = BlueprintSprite;
            blueprintLarge.tileMode = SpriteTileMode.Adaptive;
        }
    }

    private static Sprite GenerateBlueprintSprite()
    {
        return Sprite.Create(
            GenerateBlueprintTexture(), 
            new Rect(0, 0, ImageSize, ImageSize),
            new Vector2(0.5f, 0.5f));
    }

    private static Texture2D GenerateBlueprintTexture()
    {
        Texture2D tex = new(ImageSize, ImageSize);

        Color[] colors = new Color[ImageSize * ImageSize];
        for (int i = 0; i < ImageSize; i++)
        {
            for (int j = 0; j < ImageSize; j++)
            {
                if (j is 0 or ImageSize - 1 || i is 0 or ImageSize - 1)
                {
                    colors[i * ImageSize + j] = Color.white;
                }
                else
                {
                    colors[i * ImageSize + j] = Colors.BlueprintColor;
                }
            }
        }
        
        tex.SetPixels(colors);
        tex.Apply();

        return tex;
    }
}