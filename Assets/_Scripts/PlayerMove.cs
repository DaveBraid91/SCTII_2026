using System;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float sideSpeed;
    [Header("Rotation"), Range(15, 360)]
    [SerializeField] private float rotationSpeed;
    [Header("Vertical Stuff")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float stickToGroundVelocity;

    private CharacterController _characterController;

    private Vector3 _playerVelocity;
    private float _verticalVelocity;

    private bool _isJumping = false;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateMoveVelocity();
        UpdateVerticalVelocity();
        //UpdateSlideVelocity;

        ApplyTotalVelocity();

        //Crouch;
        UpdateRotation();
    }

    

    #region MOVEMENT
    private void ApplyTotalVelocity()
    {
        var totalVelocity = _playerVelocity + _verticalVelocity * Vector3.up;
        _characterController.Move(totalVelocity * Time.deltaTime);
    }

    private void UpdateMoveVelocity()
    {
        var xInput = Input.GetAxis("Horizontal");
        var yInput = Input.GetAxis("Vertical");

        var input = xInput * transform.right + yInput * transform.forward;

        if(input.sqrMagnitude > 1 ) input.Normalize();

        input = new Vector3(input.x * sideSpeed, 0 , input.z * forwardSpeed);

        _playerVelocity = input;
    }

    private void UpdateVerticalVelocity()
    {
        if (Input.GetAxisRaw("Jump") > 0.5f && _characterController.isGrounded && !_isJumping)
        {
            _isJumping = true;
            _verticalVelocity = jumpForce;
        }

        if (_isJumping && _characterController.isGrounded && _characterController.velocity.y < 0)
        {
            _isJumping = false;
        }

        if(!_isJumping && _characterController.isGrounded && _characterController.velocity.y < 0)
            _verticalVelocity = stickToGroundVelocity;

        _verticalVelocity -= gravity * Time.deltaTime;
    }
    #endregion

    private void UpdateRotation()
    {
        var mouseInput = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseInput * rotationSpeed * Time.deltaTime, 0);
    }
}
