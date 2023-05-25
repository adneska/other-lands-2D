using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class PlayerMovementHandler : MonoBehaviour
{
    public static PlayerMovementHandler Instance { get; private set; } 


    [SerializeField] private float _Speed = 5f;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private string currentState;   
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;


    //animation states
    const string IdleAnimation = "IdleAnimation";
    const string WalkAnimation = "WalkAnimation";


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
            0.1f);
        if (_movementInput.x != 0 || _movementInput.y != 0)
        {
            ChangeAnimationState(WalkAnimation);
        }
        else
        {            
            ChangeAnimationState(IdleAnimation);
        }
        _rigidbody.velocity = _smoothedMovementInput * _Speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        _animator.Play(newState);
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
