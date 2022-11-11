using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }
    void Start() => ChangeState(GameState.Start);

    [SerializeField] private int _maxhealth;
    [SerializeField] private int _health;
    [SerializeField] private int _totalbread;
    [SerializeField] private int _bread;
    [SerializeField] private bool _shield;
    public void SetTotalBread(int i)
    {
        _totalbread = i;
    }
    public int getTotalBread()
    {
        return _totalbread;
    }
    public int getBread()
    {
        return _bread;
    }
    public int getMaxHealth()
    {
        return _maxhealth;
    }
    public int getHealth()
    {
        return _health;
    }
    public bool getShield()
    {
        return _shield;
    }
    public void ChangeState(GameState newState)
    {
        Debug.Log($"New State: {newState}");
        OnBeforeStateChanged?.Invoke(newState);
        State = newState;
        switch (newState)
        {
            case GameState.Start:
                HandleStart();
                break;
            case GameState.Spawn:
                HandleSpawn();
                break;
            case GameState.Death:
                HandleDeath();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Loss:
                HandleLoss();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnAfterStateChanged?.Invoke(newState);
    }
    private void HandleStart()
    {
        Time.timeScale = 1f;
        _health = _maxhealth;
        _bread = 0;
        _shield = false;
        CheckpointManager.Instance.AddCheckpoints();
        UnitManager.Instance.setCharacter(Character.Duck);
        ChangeState(GameState.Spawn);
    }
    private void HandleSpawn()
    {
        UnitManager.Instance.SpawnCharacter();
    }
    private void HandleDeath()
    {
        if (_shield)
        {
            ShieldDisable();
            UnitManager.Instance.Respawn();
            return;
        }
        DecreaseHealth();
        if (_health > 0)
        {
            UnitManager.Instance.Respawn();
            return;
        }
        UnitManager.Instance.CharacterDeath();
        ChangeState(GameState.Loss);
    }   
    private void HandleWin()
    {
        Time.timeScale = 0f;
    }
    private void HandleLoss()
    {
        Time.timeScale = 0f;
    }

    public event Action onIncreaseBread;
    public void IncreaseBread()
    {
        _bread += 1;
        if (onIncreaseBread != null)
        {
            onIncreaseBread();
        }
    }
    public event Action onIncreaseHealth;
    public void IncreaseHealth()
    {
        if (onIncreaseHealth != null)
        {
            onIncreaseHealth();
        }
        _health += 1;
        
    }
    public event Action onDecreaseHealth;
    public void DecreaseHealth()
    {
        if (onDecreaseHealth != null)
        {
            onDecreaseHealth();
        }
        _health -= 1;
    }
    public event Action onShieldEnable;
    public void ShieldEnable()
    {
        if (onShieldEnable != null)
        {
            onShieldEnable();
        }
        _shield = true;
    }
    public event Action onShieldDisable;
    public void ShieldDisable()
    {
        if (onShieldDisable != null)
        {
            onShieldDisable();
        }
        _shield = false;
    }
}

[SerializeField]
public enum GameState
{
    Start = 0,
    Spawn = 1,
    Death = 2,
    Win = 3,
    Loss = 4,
}