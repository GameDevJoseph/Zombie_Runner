using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 10f, _jumpHeight = 30f, _gravity = 1f, _climbSpeed = 2f;
    [SerializeField] Transform _rollEndPosition;
    Vector3 _direction, _velocity;

    float _yVelocity;
    PlayerActionMap _playerInput;
    CharacterController _controller;
    Animator _anim;
    bool _isOnLedge = false;
    bool _isRolling = false;
    bool _isOnLadder = false;
    bool _hasMedKit = false;
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
        if (GameManager.Instance.CheckForLevelCompletion() == true)
        {
            _anim.SetFloat("Move", 0f);
            return;
        }
        
        CalculateMovement();

        if (_playerInput.Player.ClimbUp.WasPerformedThisFrame() && _isOnLedge)
        {
            _anim.SetBool("IsGrabbingLedge", false);
            _anim.SetBool("IsClimbingLedge", true);
        }

        if(_yVelocity <= -20f)
        {
            _yVelocity = -20f;
        }

    }

    void CalculateMovement()
    {
        if (_controller.enabled == false)
            return;

        if (_isRolling)
            return;

        float horizontal = _playerInput.Player.Move.ReadValue<Vector3>().x;

        if (_isOnLadder)
        {
            float vertical = _playerInput.Player.Move.ReadValue<Vector3>().y;
            _direction = new Vector3(0, vertical, 0);
            _anim.SetFloat("LadderClimb", Mathf.Abs(vertical));
            _velocity = _direction * _climbSpeed;
            

            if (_playerInput.Player.Jump.WasPerformedThisFrame())
            {
                _isOnLadder = false;
                _anim.SetBool("IsClimbingLadder", false);
                _anim.SetFloat("Move", Mathf.Abs(horizontal));
            }

            _controller.Move(_velocity * Time.deltaTime);
        }
        else if (_controller.isGrounded)
        {
            _direction = new Vector3(horizontal, 0, 0);
            _velocity = _direction * _speed;
            _anim.SetFloat("Move", Mathf.Abs(horizontal));
            _anim.SetBool("IsJumping", false);
            _anim.SetBool("HasFinishLadder", false);

            if (_playerInput.Player.Jump.WasPerformedThisFrame())
            {
                _yVelocity = _jumpHeight;
                _anim.SetBool("IsJumping", true);
            }

            if (_playerInput.Player.Roll.WasPerformedThisFrame())
            {
                _anim.SetBool("IsRolling", true);
                _isRolling = true;
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
        _isOnLadder = true;
        transform.position = climbPos;
        _anim.SetBool("IsClimbingLadder", true);
        _anim.SetBool("IsJumping", false);
        _ladderStandPosition = standPos;
        _yVelocity = 0;

        var facing = transform.localEulerAngles;
        facing.y = 90;
        transform.localEulerAngles = facing;

    }

    public void SetLadderStandPosition()
    {
        _yVelocity = 0;
        _controller.enabled = true;
        transform.position = _ladderStandPosition;
        _anim.SetBool("IsClimbingLadder", false);
    }

    public void SetStandFromRollPosiiton()
    {
        transform.position = _rollEndPosition.position;
        _anim.SetBool("IsRolling", false);
        _isRolling = false;
    }


    public void GettingOffLadder()
    {
        _anim.SetBool("HasFinishLadder", true);
        _controller.enabled = false;
        _isOnLadder = false;
    }

    public bool CheckForMeditKit()
    {
        return _hasMedKit;
    }

    public void PickedUpMedkit()
    {
        _hasMedKit = true;
    }

    public void RespawnPlayer()
    {
        transform.position = GameManager.Instance.RespawnPlayer().position;
    }

}
