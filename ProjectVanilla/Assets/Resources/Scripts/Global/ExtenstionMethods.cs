using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ExtensionMethods
{
    /// <summary>
    ///     IB: will recusrively go through a transform and fild if a child exists
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="goName"></param>
    /// <returns></returns>
    public static Transform FindAnyChild(this Transform trans, string goName)
    {
        if (trans == null)
            return null;

        foreach (Transform child in trans)
        {
            if (child.name == goName) return child;

            if (RecursiveFindChild(child, goName) != null) return RecursiveFindChild(child, goName);
        }

        return null;
    }

    public static bool DestroyIfNotNull(this GameObject go)
    {
        try
        {
            if (go != null)
            {
                Object.Destroy(go);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Debug.Log("Error in ExtensuionMethods.DestroyIfNotNull: " + ex.Message);
            return false;
        }
    }

    public static List<Transform> FindChildrenByTag(this Transform trans, string tag, bool recursive = true)
    {
        var children = new List<Transform>();

        foreach (Transform child in trans)
        {
            if (child.tag == tag) children.Add(child);

            if (recursive)
                children.AddRange(FindChildrenByTag(child, tag, recursive));
        }

        return children;
    }

    public static List<Transform> FindChildrenByType<T>(this Transform trans, T component, bool recursive = true)
    {
        var children = new List<Transform>();

        foreach (Transform child in trans)
        {
            if (child.GetComponent<T>() != null) children.Add(child);

            if (recursive)
                children.AddRange(FindChildrenByType(child, component, recursive));
        }

        return children;
    }

    private static Transform RecursiveFindChild(Transform t, string goName)
    {
        foreach (Transform child in t)
            if (child.name == goName)
            {
                return child;
            }
            else
            {
                if (RecursiveFindChild(child, goName) != null) return RecursiveFindChild(child, goName);
            }

        return null;
    }

    private static List<Transform> RecursiveFindChildrenByTag(Transform t, string tag)
    {
        var children = new List<Transform>();

        foreach (Transform child in t)
        {
            if (child.name == tag)
                children.Add(child);

            children.AddRange(RecursiveFindChildrenByTag(child, tag));
        }

        return children;
    }
}