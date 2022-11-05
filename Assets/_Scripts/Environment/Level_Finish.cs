using UnityEngine;
using System;

public class Level_Finish : MonoBehaviour
{
    private bool trigger = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && trigger)
        {
            GameManager.Instance.ChangeState(GameState.Win);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(UnitManager.Instance.spawned.GetComponent<Collider>()))
        {
            trigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.Equals(UnitManager.Instance.spawned.GetComponent<Collider>()))
        {
            trigger = false;
        }
    }
}
