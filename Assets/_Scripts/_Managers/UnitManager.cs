using UnityEngine;
using System.Threading.Tasks;

public class UnitManager : StaticInstance<UnitManager>
{

    public Character _selectedCharacter;
    public CharacterBase spawned;
    public ScriptableCharacter CharacterScriptable;
    [SerializeField] private float RespawnDelay = 0.2f;
    public void setCharacter(Character c) => _selectedCharacter = c;
    public void SpawnCharacter()
    {
        var pos = CheckpointManager.Instance.currentCheckpoint.transform.position;
        CharacterScriptable = ResourceSystem.Instance.GetCharacter(_selectedCharacter);
        spawned = Instantiate(CharacterScriptable.Prefab, pos, Quaternion.identity, transform);
        var stats = CharacterScriptable.BaseStats;
        spawned.SetStats(stats);
        Camera_Follow.Instance.target = spawned.transform;
    }
    public void CharacterDeath()
    {
        spawned.Death();
        Camera_Follow.Instance.target = CheckpointManager.Instance.currentCheckpoint.transform;
    }
    public async void Respawn()
    {
        CharacterDeath();
        await Task.Delay((int)(1000 * RespawnDelay));
        SpawnCharacter();
    }
}