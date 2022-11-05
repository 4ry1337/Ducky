using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isChecked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(UnitManager.Instance.spawned.GetComponent<Collider>()))
        {
            if (isChecked) return;
            isChecked = true;
            CheckpointManager.Instance.changeCurrent(this);
        }
    }
}
