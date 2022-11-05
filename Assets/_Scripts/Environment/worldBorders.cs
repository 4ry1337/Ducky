using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldBorders : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(UnitManager.Instance.spawned.gameObject))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.ChangeState(GameState.Death);
        }
    }
}
