using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    Rigidbody _rb;
    Vector3 initialPos;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            _rb.velocity = Vector3.zero;
            transform.position = initialPos;
        }
    }
}
