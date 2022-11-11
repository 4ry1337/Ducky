using System;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public Stats Stats { get; private set; }
    public virtual void SetStats(Stats stats) => Stats = stats;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private AudioClip _sound;
    [SerializeField] private AudioClip _death;
    private Vector3 _input;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _jumpForce = Stats._jumpForce;
        _speed = Stats._speed;
    }
    void Update()
    {
        GatherInputs();
        handleGrounding();
        handleWalking();
        handleJumping();
        Look();
    }
    #region Controller
    void GatherInputs()
    {
        _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioSystem.Instance?.PlaySound(_sound, 0.5f);
        }
    }
    public void Death()
    {
        AudioSystem.Instance?.PlaySound(_death, 0.5f);
    }
    #region Grounded
    [Header("Detection")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _grounderOffset = -1, _grounderRadius = 0.2f;
    public bool IsGrounded = true;
    public static event Action OnTouchedGround;
    private readonly Collider[] _ground = new Collider[1];
    private void handleGrounding()
    {
        var grounded = Physics.OverlapSphereNonAlloc(transform.position + new Vector3(0, _grounderOffset), _grounderRadius, _ground, _groundMask) > 0;

        if (!IsGrounded && grounded)
        {
            IsGrounded = true;
            _hasJumped = false;
            OnTouchedGround?.Invoke();
        }
        else if (IsGrounded && !grounded)
        {
            IsGrounded = false;
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
    private void Look()
    {
        if (_input != Vector3.zero)
        {
            var relative = (transform.position + _input) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _rotSpeed * Time.deltaTime);
        }
    }
    private void handleWalking()
    {
        var _idealSpeed = _input;
        _idealSpeed.Normalize();
        _idealSpeed *= _speed;
        _idealSpeed.y = _rb.velocity.y;
        _rb.velocity = Vector3.MoveTowards(_rb.velocity, _idealSpeed, 100 * Time.deltaTime);
    }
    #endregion
    #region Jump
    [Header("Jump")]
    [SerializeField] private bool _hasJumped;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fallMultiplier = 1.2f;
    [SerializeField] private float _jumpVelocityFalloff = 4;
    [SerializeField] private float _coyoteTime = 1f;
    private float _timeLeftGrounded = -10f;
    private void handleJumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if((IsGrounded || Time.time < _timeLeftGrounded + _coyoteTime) && !_hasJumped){
                _rb.velocity = new Vector3(_rb.velocity.x, _jumpForce, _rb.velocity.z);
                _hasJumped = true;
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