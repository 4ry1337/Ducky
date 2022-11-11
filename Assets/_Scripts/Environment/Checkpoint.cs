using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isChecked = false;
    [SerializeField] private AudioClip _sound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(UnitManager.Instance.spawned.GetComponent<Collider>()))
        {
            if (isChecked) return;
            isChecked = true;
            ChangeColor();
            AudioSystem.Instance.PlaySound(_sound);
            CheckpointManager.Instance.changeCurrent(this);
        }
    }
    private void ChangeColor()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].SetColor("_EmissionColor", new Color(0f, 0f, 2f));
    }
}
