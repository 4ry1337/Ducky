using System.Collections;
using UnityEngine;

public class Heart : ItemBase
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!(GameManager.Instance.getMaxHealth() == GameManager.Instance.getHealth()))
            {
                GameManager.Instance.IncreaseHealth();
                base.OnTriggerEnter(other);
            }
        }
    }
}