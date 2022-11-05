using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;

public class LevelManager : PersistentSingleton<LevelManager>
{
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    private float _target;
    public async void LoadScene(int sceneBuild)
    {
        _target = 0;
        _progressBar.fillAmount = 0;
        var operation = SceneManager.LoadSceneAsync(sceneBuild);;
        operation.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do {
            await Task.Delay(100);
            _target = operation.progress;
        } while (operation.progress < 0.9f);

        await Task.Delay(100);

        operation.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
    }

    private void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 2 * Time.deltaTime);
    }
    public void ReloadCurrentLevel()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        if(SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
        {
            LoadScene(0);
        }else
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
