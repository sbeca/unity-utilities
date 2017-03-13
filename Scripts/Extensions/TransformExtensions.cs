using UnityEngine;
using System.Collections.Generic;

public static class TransformExtensions
{
    /// <summary>
    /// Reset the Transform's world position, rotation, and scale.
    /// </summary>
    public static void Reset(this Transform t)
    {
        Transform parent = t.parent;
        t.parent = null;
        t.ResetLocal();
        t.parent = parent;
    }

    /// <summary>
    /// Reset the Transform's local position, rotation, and scale.
    /// </summary>
    public static void ResetLocal(this Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localScale = Vector3.one;
        t.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Set the Transform's world X position.
    /// </summary>
    public static void SetPositionX(this Transform t, float x)
    {
        t.position = t.position.WithX(x);
    }

    /// <summary>
    /// Set the Transform's world Y position.
    /// </summary>
    public static void SetPositionY(this Transform t, float y)
    {
        t.position = t.position.WithY(y);
    }

    /// <summary>
    /// Set the Transform's world Z position.
    /// </summary>
    public static void SetPositionZ(this Transform t, float z)
    {
        t.position = t.position.WithZ(z);
    }

    /// <summary>
    /// Set the Transform's local X position.
    /// </summary>
    public static void SetLocalPositionX(this Transform t, float x)
    {
        t.localPosition = t.localPosition.WithX(x);
    }

    /// <summary>
    /// Set the Transform's local Y position.
    /// </summary>
    public static void SetLocalPositionY(this Transform t, float y)
    {
        t.localPosition = t.localPosition.WithY(y);
    }

    /// <summary>
    /// Set the Transform's local Z position.
    /// </summary>
    public static void SetLocalPositionZ(this Transform t, float z)
    {
        t.localPosition = t.localPosition.WithZ(z);
    }

    /// <summary>
    /// Translate the Transform's world X position.
    /// </summary>
    public static void TranslatePositionX(this Transform t, float x)
    {
        t.position = t.position.WithIncrementedX(x);
    }

    /// <summary>
    /// Translate the Transform's world Y position.
    /// </summary>
    public static void TranslatePositionY(this Transform t, float y)
    {
        t.position = t.position.WithIncrementedY(y);
    }

    /// <summary>
    /// Translate the Transform's world Z position.
    /// </summary>
    public static void TranslatePositionZ(this Transform t, float z)
    {
        t.position = t.position.WithIncrementedZ(z);
    }

    /// <summary>
    /// Translate the Transform's local X position.
    /// </summary>
    public static void TranslateLocalPositionX(this Transform t, float x)
    {
        t.localPosition = t.localPosition.WithIncrementedX(x);
    }

    /// <summary>
    /// Translate the Transform's local Y position.
    /// </summary>
    public static void TranslateLocalPositionY(this Transform t, float y)
    {
        t.localPosition = t.localPosition.WithIncrementedY(y);
    }

    /// <summary>
    /// Translate the Transform's local Z position.
    /// </summary>
    public static void TranslateLocalPositionZ(this Transform t, float z)
    {
        t.localPosition = t.localPosition.WithIncrementedZ(z);
    }

    /// <summary>
    /// Set the Transform's world X euler rotation.
    /// </summary>
    public static void SetRotationX(this Transform t, float x)
    {
        t.eulerAngles = t.eulerAngles.WithX(x);
    }

    /// <summary>
    /// Set the Transform's world Y euler rotation.
    /// </summary>
    public static void SetRotationY(this Transform t, float y)
    {
        t.eulerAngles = t.eulerAngles.WithY(y);
    }

    /// <summary>
    /// Set the Transform's world Z euler rotation.
    /// </summary>
    public static void SetRotationZ(this Transform t, float z)
    {
        t.eulerAngles = t.eulerAngles.WithZ(z);
    }

    /// <summary>
    /// Set the Transform's local X euler rotation.
    /// </summary>
    public static void SetLocalRotationX(this Transform t, float x)
    {
        t.localEulerAngles = t.localEulerAngles.WithX(x);
    }

    /// <summary>
    /// Set the Transform's local Y euler rotation.
    /// </summary>
    public static void SetLocalRotationY(this Transform t, float y)
    {
        t.localEulerAngles = t.localEulerAngles.WithY(y);
    }

    /// <summary>
    /// Set the Transform's local Z euler rotation.
    /// </summary>
    public static void SetLocalRotationZ(this Transform t, float z)
    {
        t.localEulerAngles = t.localEulerAngles.WithZ(z);
    }

    /// <summary>
    /// Set the Transform's local X scale.
    /// </summary>
    public static void SetLocalScaleX(this Transform t, float x)
    {
        t.localScale = t.localScale.WithX(x);
    }

    /// <summary>
    /// Set the Transform's local Y scale.
    /// </summary>
    public static void SetLocalScaleY(this Transform t, float y)
    {
        t.localScale = t.localScale.WithY(y);
    }

    /// <summary>
    /// Set the Transform's local Z scale.
    /// </summary>
    public static void SetLocalScaleZ(this Transform t, float z)
    {
        t.localScale = t.localScale.WithZ(z);
    }

    /// <summary>
    /// Destroy all the children of this Transform.
    /// </summary>
    public static void DestroyChildren(this Transform t)
    {
        // Add children to list before destroying otherwise GetChild(i) may bomb out
        List<Transform> children = new List<Transform>();

        for (int i = 0; i < t.childCount; i++)
        {
            Transform child = t.GetChild(i);
            children.Add(child);
        }

        for (int i = 0; i < children.Count; i++)
        {
            Object.Destroy(children[i].gameObject);
        }
    }

    /// <summary>
    /// DestroyImmediate all the children of this Transform.
    /// </summary>
    public static void DestroyChildrenImmediate(this Transform t)
    {
        // Add children to list before destroying otherwise GetChild(i) may bomb out
        List<Transform> children = new List<Transform>();

        for (int i = 0; i < t.childCount; i++)
        {
            Transform child = t.GetChild(i);
            children.Add(child);
        }

        for (int i = 0; i < children.Count; i++)
        {
            Object.DestroyImmediate(children[i].gameObject);
        }
    }

    /// <summary>
    /// Find the child of this Transform with a specified name.
    /// </summary>
    public static Transform RecursiveSearch(this Transform t, string name)
    {
        return RecursiveSearchRecurse(t, name);
    }

    private static Transform RecursiveSearchRecurse(Transform current, string name)
    {
        if (current.name == name)
        {
            return current;
        }
        else
        {
            for (int i = 0; i < current.childCount; ++i)
            {
                Transform found = RecursiveSearchRecurse(current.GetChild(i), name);

                if (found != null)
                {
                    return found;
                }
            }
        }

        return null;
    }
}
