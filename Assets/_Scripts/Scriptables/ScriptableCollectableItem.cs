using System;
using UnityEngine;
[CreateAssetMenu(fileName = "New Collectable Item", menuName = "Items", order =51)]
public class ScriptableCollectableItem : ScriptableObject
{
    public ItemType _itemType;
    public ItemBase Prefab;
}

[Serializable]
public enum ItemType
{
    bread = 1,
    heart = 2,
    shield = 3,
}