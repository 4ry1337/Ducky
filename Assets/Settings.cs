using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    List<int> widths = new List<int>() { 1366, 1920, 2560 };
    List<int> heights = new List<int>() { 768, 1080, 1440 };
    
    public void SetScreenSize(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);
    }
    public void SetFullScreen(bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;
    }
}
