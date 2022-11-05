using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Wiggle : MonoBehaviour
{
    [SerializeField] private Transform _initial;
    [SerializeField] private bool _withTimer = false;
    [SerializeField] private bool _timer = false;
    [SerializeField] private float _time = 1;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _fromRotAngle = -45;
    [SerializeField] private float _toRotAngle = 45;
    
    void Start()
    {
        _initial = transform;
        if (_withTimer) _timer = true;
    }
    void FixedUpdate()
    {
        if (_withTimer)
        {
            if (_timer)
            {
                _time -= Time.smoothDeltaTime;
                if (_time >= 0)
                {
                    float rZ = Mathf.SmoothStep(_fromRotAngle, _toRotAngle, Mathf.PingPong(Time.time * _speed, 1));
                    transform.rotation = Quaternion.Euler(0, 0, rZ);
                }
                else
                {
                    _timer = false;
                }
            }
            else
            {
                float rZ = Mathf.SmoothStep(transform.rotation.z, _initial.rotation.z, Mathf.PingPong(Time.time * _speed, 1));
                transform.rotation = Quaternion.Euler(0, 0, rZ);
            }
        }
        else
        {
            float rZ = Mathf.SmoothStep(_fromRotAngle, _toRotAngle, Mathf.PingPong(Time.time * _speed, 1));
            transform.rotation = Quaternion.Euler(0, 0, rZ);
        }
    }
}
