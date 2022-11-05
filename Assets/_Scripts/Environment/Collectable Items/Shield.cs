using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : ItemBase
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (UnitManager.Instance.spawned)
        {
            if (!(GameManager.Instance.getShield()))
            {
                Destroy(gameObject);
                GameManager.Instance.ShieldEnable();
            }
        }
    }
}
