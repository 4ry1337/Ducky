using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : Singleton<LevelSelector>
{
    [SerializeField] private LevelButton levelButton;
    private int numberOfscenes;
    void Start()
    {
        numberOfscenes = SceneManager.sceneCountInBuildSettings;
        for (int i=1; i<numberOfscenes; i++)
        {
            LevelButton newlevelButton = levelButton;
            newlevelButton.levelIndex = i;
            Instantiate(newlevelButton, transform);
        }
    }
}
