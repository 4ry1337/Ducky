using UnityEngine;

public static class Helpers
{
    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }

    internal static void DestroyChildren(UnitManager unitManager)
    {
        throw new System.NotImplementedException();
    }
}
