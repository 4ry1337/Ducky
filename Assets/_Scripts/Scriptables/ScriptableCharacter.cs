using System;
using UnityEngine;

[CreateAssetMenu(fileName="New Character", menuName ="Character", order = 51)]
public class ScriptableCharacter : ScriptableObject
{
    [SerializeField] private Stats _stats;
    public Stats BaseStats => _stats;

    //used in game
    public CharacterBase Prefab;
    public Character _character;
}

[Serializable]
public struct Stats
{
    public float _speed;
    public float _jumpForce;
}

[Serializable]
public enum Character
{
    Duck = 0,
    Cat = 1,
    Dog = 2,
}