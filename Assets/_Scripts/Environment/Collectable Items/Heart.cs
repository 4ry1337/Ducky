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
                Destroy(gameObject);
                GameManager.Instance.IncreaseHealth();
            }
        }
    }
}