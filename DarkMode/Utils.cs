using UnityEngine;

namespace DarkMode;

public static class Utils
{
    // -- Basic plugin -- //
    public static void Log(object msg)
    {
        Debug.Log($"<color=#00FF77>{DarkMode.ModName}: {msg}</color>");
    }
    
    
    // -- General Unity -- //
    public static T? FindComponent<T>(string path, Transform? parent = null)
    {
        if (parent is not null)
        {
            return parent.Find(path) is { } child
                ? child.GetComponent<T>() 
                : default;
        }

        return GameObject.Find(path) is { } go 
            ? go.GetComponent<T>() 
            : default;
    }

    public static List<T> FindComponents<T>(string path, Transform? parent = null)
    {
        List<T> l = new();

        Transform? tx;
        if (parent is not null && parent.Find(path) is { } child1) 
        {
            tx = child1;
        }
        else if (GameObject.Find(path) is { } child2)
        {
            tx = child2.transform;
        }
        else
        {
            tx = null;
        }

        if (tx is not null)
        {
            foreach (Transform t in tx)
            {
                if (t.GetComponent<T>() is { } comp)
                {
                    l.Add(comp);
                }
            }
        }
        
        return l;
    }

    public static void Refresh(GameObject go)
    {
        if (go.activeSelf)
        {
            go.SetActive(false);
            go.SetActive(true);
        }
    }

    public static Color AdjustColor(Color color, float adjust)
    {
        return new Color(color.r * adjust, color.g * adjust, color.b * adjust, color.a);
    }
}