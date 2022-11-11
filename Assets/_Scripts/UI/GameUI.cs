using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Threading.Tasks;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Image _shield;
    [SerializeField] private Image _heart;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _lossSound;
    private void OnDestroy() => GameManager.OnAfterStateChanged -= OnStateChanged;
    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.Win)
        {
            AudioSystem.Instance.PlaySound(_winSound);
            _score.text = GameManager.Instance.getBread().ToString() + " / " + GameManager.Instance.getTotalBread().ToString();
            _win.SetActive(true);
            LoadCompleteness();
        }
        else if (newState == GameState.Loss)
        {
            AudioSystem.Instance.PlaySound(_lossSound);
            _loss.SetActive(true);
        }
    }
    private void Start()
    {
        _stars.fillAmount = 0f;
        healthIndex = GameManager.Instance.getMaxHealth() - 1;
        for (int i=0; i<GameManager.Instance.getMaxHealth(); i++)
        {
            hearts.Add(Instantiate(_heart, _container));
        }
        _shieldClone = Instantiate(_shield, _container);
        tempAlpha = _shieldClone.color;
        DisableShield();
        GameManager.OnAfterStateChanged += OnStateChanged;
        GameManager.Instance.onIncreaseHealth += IncreaseHeart;
        GameManager.Instance.onDecreaseHealth += DecreaseHeart;
        GameManager.Instance.onShieldEnable += EnableShield;
        GameManager.Instance.onShieldDisable += DisableShield;
        GameManager.Instance.onIncreaseBread += IncreaseBread;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (_gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if(_completeness != 0)
        {
            _stars.fillAmount = Mathf.MoveTowards(_stars.fillAmount, _completeness, 3 * Time.unscaledDeltaTime);
        }
    }
    #region ui
    [Header("Health")]
    private int healthIndex;
    [SerializeField] private List<Image> hearts;
    private void IncreaseHeart()
    {
        healthIndex++;
        Color tempColor = hearts.ElementAt(healthIndex).color;
        tempColor.a = 1f;
        hearts.ElementAt(healthIndex).color = tempColor;
    }
    private void DecreaseHeart()
    {
        Color tempColor = hearts.ElementAt(healthIndex).color;
        tempColor.a = 0.1f;
        hearts.ElementAt(healthIndex).color = tempColor;
        healthIndex--;
    }

    [Header("Shield")]
    [SerializeField] private Image _shieldClone;
    private Color tempAlpha;
    private void EnableShield()
    {
        tempAlpha.a = 1f;
        _shieldClone.color = tempAlpha;
    }
    private void DisableShield()
    {
        tempAlpha.a = 0.1f;
        _shieldClone.color = tempAlpha;
    }
    [Header("Bread")]
    [SerializeField] private TMP_Text text;
    private int _bread = 0;
    private void IncreaseBread()
    {
        _bread += 1;
        text.text = _bread.ToString();
    }
    #endregion
    #region pause menu
    [Header("Pause Menu")]
    [SerializeField] private GameObject _pauseMenu;
    private static bool _gameIsPaused = false;
    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;
    }
    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }
    #endregion
    #region finish
    [Header("Finish")]
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _loss;
    #region win
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Image _stars;
    [SerializeField] private float _completeness;
    private void LoadCompleteness()
    {
        _completeness = (float) GameManager.Instance.getBread() / (float)GameManager.Instance.getTotalBread(); 
    }
    #endregion
    #endregion

}