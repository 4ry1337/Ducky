using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : ItemBase
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        GameManager.Instance.IncreaseBread();
    }
}
