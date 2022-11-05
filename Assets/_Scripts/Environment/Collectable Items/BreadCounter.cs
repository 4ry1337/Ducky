using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadCounter : MonoBehaviour
{
    private int i = 0;
    public void Start()
    {
        foreach (Transform child in transform)
        {
            if(child.GetComponent<Bread>() != null)
            {
                i++;
            }
        }
        GameManager.Instance.SetTotalBread(i);
    }
}
