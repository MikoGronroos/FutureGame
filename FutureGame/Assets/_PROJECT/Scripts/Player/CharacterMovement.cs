using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, IMovement
{

    [SerializeField] private float walkingSpeed;

    [SerializeField] private float airControlPercent;

    [SerializeField] private float crouchingSpeed;
    [SerializeField] private bool isCrouching;

    [SerializeField] private bool isRunning;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float runningStaminaReduce;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpingStaminaReduce;

    [SerializeField] private Animator animator;

    [SerializeField] private bool isGrounded;


    private CharacterOwner _charOwner;
    private GroundCheck _groundCheck;
    private Rigidbody _rigidbody;

    //Input
    private float _horizontalInput;
    private float _verticalInput;

    [SerializeField] private bool _hasJumped;

    private float CurrentTargetForce
    {
        get
        {
            if (isCrouching)
            {
                return crouchingSpeed;
            }

            if (isRunning)
            {
                return runningSpeed;
            }

            return walkingSpeed;
        }
    }

    private void OnDisable()
    {
        _horizontalInput = 0;
        _verticalInput = 0;
        isCrouching = false;
        isRunning = false;
        AnimationHandler(_horizontalInput, _verticalInput);
    }

    private void Awake() 
    {
        _groundCheck = GetComponent<GroundCheck>();
        _charOwner = GetComponent<CharacterOwner>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = _groundCheck.Grounded();
        AnimationHandler(_horizontalInput, _verticalInput);
        HandleInput();
    }

    private void AnimationHandler(float horizontalInput, float verticalInput)
    {
        animator.SetInteger("horizontalInput", (int)horizontalInput);
        animator.SetInteger("verticalInput", (int)verticalInput);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isCrouching", isCrouching);
    }

    private void FixedUpdate()
    {
        Movement(_horizontalInput, _verticalInput);
        AddJumpVelocity();
    }

    private void HandleInput()
    {
        _horizontalInput = _charOwner.Input.HorizontalInput();
        _verticalInput = _charOwner.Input.VerticalInput();
        isRunning = _charOwner.Input.RunInput();
        _hasJumped = _charOwner.Input.JumpInput();
        isCrouching = _charOwner.Input.CrouchInput();
    }

    public void Movement(float horizontalInput, float verticalInput)
    {
        if (Mathf.Abs(horizontalInput) - Mathf.Epsilon > 0 || Mathf.Abs(verticalInput) - Mathf.Epsilon > 0)
        {
            Transform t = transform;
            Vector3 desiredMove = t.forward * verticalInput + t.right * horizontalInput;

            desiredMove.x *= (isGrounded ? CurrentTargetForce : CurrentTargetForce * airControlPercent);
            desiredMove.z *= (isGrounded ? CurrentTargetForce : CurrentTargetForce * airControlPercent);

            if (_rigidbody.velocity.sqrMagnitude < (CurrentTargetForce * CurrentTargetForce))
            {
                _rigidbody.AddForce(desiredMove, ForceMode.Impulse);
            }
        }
    }

    private void AddJumpVelocity()
    {
        if (isGrounded)
        {
            _rigidbody.drag = 5f;

            if (_hasJumped)
            {
                _rigidbody.drag = 0f;
                Vector3 velocity = _rigidbody.velocity;
                velocity = new Vector3(velocity.x, 0f, velocity.z);
                _rigidbody.velocity = velocity;
                _rigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
                _charOwner.CharacterStats.CurrentStamina -= jumpingStaminaReduce;
            }
        }
    }
}
