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

    [SerializeField] private bool _hasJumped;

    private CharacterOwner _charOwner;
    private GroundCheck _groundCheck;
    private Rigidbody _rigidbody;

    private float _currentSpeed;

    //Input
    private float _horizontalInput;
    private float _verticalInput;

    private float CurrentTargetForce
    {
        get
        {

            _currentSpeed = walkingSpeed;

            if (isCrouching)
            {
                _currentSpeed = crouchingSpeed;
            }

            if (isRunning)
            {
                _currentSpeed = runningSpeed;
            }

            return _currentSpeed;
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
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (_hasJumped)
        {
            AddJumpVelocity();
        }
        Movement(_horizontalInput, _verticalInput);
    }

    private void LateUpdate()
    {
        AnimationHandler(_horizontalInput, _verticalInput);
    }


    private void AnimationHandler(float horizontalInput, float verticalInput)
    {
        animator.SetBool("hasJumped", _hasJumped);
        animator.SetInteger("horizontalInput", (int)horizontalInput);
        animator.SetInteger("verticalInput", (int)verticalInput);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isCrouching", isCrouching);
    }

    private void HandleInput()
    {
        _horizontalInput = _charOwner.Input.HorizontalInput();
        _verticalInput = _charOwner.Input.VerticalInput();
        if (isGrounded && !_hasJumped)
        {
            _hasJumped = _charOwner.Input.JumpInput();
        }
        isRunning = _charOwner.Input.RunInput();
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
        _charOwner.CharacterStats.CurrentStamina -= jumpingStaminaReduce;
        _rigidbody.velocity += Vector3.up * jumpForce;
        _hasJumped = false;
    }
}
