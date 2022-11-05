using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup _uiGroup;
    [SerializeField] private float _time = 0;
    void Start()
    {
        StartCoroutine(DoSomethingAfterTenSeconds());
    }

    IEnumerator DoSomethingAfterTenSeconds()
    {
        yield return new WaitForSeconds(_time);

        _uiGroup.alpha = 0;
    }
}
