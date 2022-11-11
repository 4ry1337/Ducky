using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : Singleton<ResourceSystem>
{
    #region Player
    public List<ScriptableCharacter> Characters { get; private set; }
    [SerializeField] private Dictionary<Character, ScriptableCharacter> _CharacterDict;

    public List<ScriptableCollectableItem> Items { get; private set; }
    [SerializeField] private Dictionary<ItemType, ScriptableCollectableItem> _ItemDict;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources()
    {
        Characters = Resources.LoadAll<ScriptableCharacter>("Characters").ToList();
        _CharacterDict = Characters.ToDictionary(r => r._character, r => r);
        Items = Resources.LoadAll<ScriptableCollectableItem>("CollectableItems").ToList();
        _ItemDict = Items.ToDictionary(r => r._itemType, r => r);
    }
    public ScriptableCollectableItem GetScriptableCollectableItem(ItemType it) => _ItemDict[it];
    public ScriptableCharacter GetCharacter(Character t) => _CharacterDict[t];
}