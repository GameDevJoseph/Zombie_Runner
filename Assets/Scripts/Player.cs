using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 10f, _jumpHeight = 30f, _gravity = 1f;
    Vector3 _direction, _velocity;

    float _yVelocity;
    PlayerActionMap _playerInput;
    CharacterController _controller;
    Animator _anim;
    bool _isOnLedge = false;
    bool _isOnLadder = false;
    Vector3 _ledgeStandPosition;
    Vector3 _ladderStandPosition;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _controller = GetComponent<CharacterController>();
        _playerInput = new PlayerActionMap();
        _playerInput.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(_playerInput.Player.ClimbUp.WasPerformedThisFrame() && _isOnLedge)
        {
            _anim.SetBool("IsGrabbingLedge", false);
            _anim.SetBool("IsClimbingLedge", true);
        }

    }

     void CalculateMovement()
    {
        if (_controller.enabled == false)
            return;

        float horizontal = _playerInput.Player.Move.ReadValue<Vector2>().x;

        if (_controller.isGrounded)
        {
            _direction = new Vector3(horizontal, 0, 0);
            _velocity = _direction * _speed;
            _anim.SetFloat("Move", Mathf.Abs(horizontal));
            _anim.SetBool("IsJumping", false);

            if (_playerInput.Player.Jump.WasPerformedThisFrame())
            {
                _yVelocity = _jumpHeight;
                _anim.SetBool("IsJumping", true);
            }

            if (horizontal != 0)
            {
                var facing = transform.localEulerAngles;
                facing.y = _direction.x > 0 ? 90 : 270;
                transform.localEulerAngles = facing;
            }

        }
        else
        {
            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
    }

    public void SetLedgeGrabPosition(Vector3 handPos, Vector3 standPos)
    {
        transform.position = handPos;
        _controller.enabled = false;
        _anim.SetBool("IsGrabbingLedge", true);
        _isOnLedge = true;
        _ledgeStandPosition = standPos;
    }

    public void SetLedgeStandPosition()
    {
        transform.position = _ledgeStandPosition;
        _anim.SetBool("IsClimbingLedge", false);
        _isOnLedge = false;
        _controller.enabled = true;
    }

    public void SetLadderGrabPosition(Vector3 climbPos, Vector3 standPos)
    {
        transform.position = climbPos;
        _controller.enabled = false;
        _anim.SetBool("IsClimbingLadder", true);
        _isOnLadder = true;
        _ladderStandPosition = standPos;
    }

    public void SetLadderStandPosition()
    {
        transform.position = _ladderStandPosition;
        _anim.SetBool("IsClimbingLadder", false);
        _isOnLadder = false;
        _controller.enabled = true;
    }

    

}
