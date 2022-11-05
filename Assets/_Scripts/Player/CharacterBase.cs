using System;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public Stats Stats { get; private set; }
    public virtual void SetStats(Stats stats) => Stats = stats;

    [SerializeField] private Rigidbody _rb;
    private Vector3 _dir;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _jumpForce = Stats._jumpForce;
        _speed = Stats._speed;
    }
    void Update()
    {
        handleGrounding();
        handleWalking();
        handleJumping();
    }
    #region Controller
    #region Grounded
    [Header("Detection")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _grounderOffset;
    [SerializeField] private float _grounderRadius;
    private readonly Collider[] _ground = new Collider[1];
    public bool IsGrounded = true;
    public static event Action OnTouchedGround;
    private void handleGrounding()
    {
        var grounded = Physics.OverlapSphereNonAlloc(transform.position + new Vector3(0, _grounderOffset), _grounderRadius, _ground, _groundMask) > 0;
        if (!IsGrounded && grounded)
        {
            IsGrounded = true;
            OnTouchedGround?.Invoke();
        }
        else if (IsGrounded && !grounded)
        {
            IsGrounded = false;
            transform.SetParent(null);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, _grounderOffset), _grounderRadius);
    }
    #endregion
    #region Walk
    [Header("Walking")]
    [SerializeField] private float _speed;
    [SerializeField] private float _rotSpeed;
    private void handleWalking()
    {
        _dir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (_dir != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(_dir);
            toRotate = Quaternion.RotateTowards(transform.rotation, toRotate, _rotSpeed * Time.deltaTime);
            _rb.MoveRotation(toRotate);
        }
        _dir.Normalize();
        _dir *= _speed;
        _dir.y = _rb.velocity.y;
        _rb.velocity = _dir;
    }
    #endregion
    #region Jump
    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fallMultiplier = 1.2f;
    [SerializeField] private float _jumpVelocityFalloff = 4;
    [SerializeField] private float _coyoteTime = 1f;
    private float _timeLeftGrounded = -10f;
    private void handleJumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded || (Time.time < _timeLeftGrounded + _coyoteTime))
            {
                _rb.AddForce(transform.up * _jumpForce);
            }
        }
        if (_rb.velocity.y < _jumpVelocityFalloff || _rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * _fallMultiplier * Time.deltaTime;
        }
    }
    #endregion
    #endregion
}