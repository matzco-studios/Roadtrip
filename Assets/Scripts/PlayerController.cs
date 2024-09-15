using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private Transform _head;
    [SerializeField] private float _coyoteTimeMax = 0.21f;
    [SerializeField] private float _jumpBufferMax = 0.21f;
    [SerializeField] private float _gravityStrength = -9.81f;
    private float _coyoteTime;
    private float _jumpBuffer;
    private CharacterController _controller;
    private Vector3 _inputDir;
    private Vector3 _direction;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _gravityVelocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
    }

    private void HandleJump()
    {
        _coyoteTime -= Time.deltaTime;
        _jumpBuffer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)){
            _jumpBuffer = _jumpBufferMax;
        }

        if (_jumpBuffer>=0 && _coyoteTime>=0){
            _gravityVelocity.y = _jumpForce;
        }
    }

    private void Gravity()
    {
        _gravityVelocity.y += _gravityStrength * Time.fixedDeltaTime * ( (_gravityVelocity.y<0) ? 1.65f : 1 );
        
        _controller.Move(_gravityVelocity * Time.fixedDeltaTime);

        if (_controller.collisionFlags == CollisionFlags.Below){
            _gravityVelocity.y = 0;
            _coyoteTime = _coyoteTimeMax;
        }
    }

    private void PlayerDirection()
    {
        _inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        _direction = Quaternion.AngleAxis(_head.eulerAngles.y, Vector3.up) * _inputDir;
    }

    private void Movement()
    {
        Vector3 targetVelocity = _movementSpeed * _direction;

        _velocity += (targetVelocity-_velocity)/15;
        if (_inputDir.Equals(Vector3.zero)){
            _velocity += (Vector3.zero-_velocity)/10;
        }
        
        _controller.Move(_velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        HandleJump();
    }
    
    void FixedUpdate()
    {
        PlayerDirection();
        Movement();
        Gravity();
    }
}
