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
        float horizontal = _playerInput.Player.Move.ReadValue<Vector2>().x;

        if(_controller.isGrounded)
        {
            _direction = new Vector3(horizontal, 0, 0);
            _velocity = _direction * _speed;
            _anim.SetFloat("Move", Mathf.Abs(horizontal));
            _anim.SetBool("IsJumping", false);

            if (_playerInput.Player.Jump.WasPressedThisFrame())
            {
                _yVelocity = _jumpHeight;
                _anim.SetBool("IsJumping", true);
            }

        }else
        {
            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);

    }
}
