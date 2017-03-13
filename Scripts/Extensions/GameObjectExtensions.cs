using UnityEngine;

public static class GameObjectExtensions
{
    public static void SetLayerRecursively(this GameObject source, int layer)
    {
        source.layer = layer;

        int childCount = source.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            source.transform.GetChild(i).gameObject.SetLayerRecursively(layer);
        }
    }
}
