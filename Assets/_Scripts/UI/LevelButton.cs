using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public int levelIndex;
    public TMP_Text text;
    void Awake()
    {
        text.text = levelIndex.ToString();
    }
    public void LoadLevel()
    {
        LevelManager.Instance.LoadScene(levelIndex);
    }
}