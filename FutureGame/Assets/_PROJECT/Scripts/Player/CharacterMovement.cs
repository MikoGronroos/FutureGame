using UnityEngine;

public class CharacterMovement : MonoBehaviour, IMovement
{

    [SerializeField] private float currentSpeed;

    [SerializeField] private float walkingSpeed;

    [SerializeField] private float crouchingSpeed;
    [SerializeField] private bool isCrouching;

    [SerializeField] private bool isRunning;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float runningStaminaReduce;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpingStaminaReduce;

    [SerializeField] private float gravity;

    [SerializeField] private Animator animator;

    private Vector3 moveDirection = Vector3.zero;

    private CharacterOwner _charOwner;
    private CharacterController _controller;

    //Input
    [SerializeField] private float _horizontalInput;
    [SerializeField] private float _verticalInput;

    private bool _hasJumped;

    private void Awake()
    {
        _charOwner = GetComponent<CharacterOwner>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleInput();
        Crouch();
        SetSpeed();
        Movement(_horizontalInput, _verticalInput);
    }

    private void Crouch()
    {
        if (isCrouching)
        {
            animator.SetBool("isCrouching", true);
        }
        else
        {
            animator.SetBool("isCrouching", false);
        }
    }

    private void SetSpeed()
    {
        if (isRunning)
        {
            _charOwner.CharacterStats.CurrentStamina -= runningStaminaReduce * Time.deltaTime;
            currentSpeed = runningSpeed;
        }else if (isCrouching)
        {
            currentSpeed = crouchingSpeed;
        }
        else
        {
            currentSpeed = walkingSpeed;
        }
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

        animator.SetInteger("horizontalInput", (int)horizontalInput);
        animator.SetInteger("verticalInput", (int)verticalInput);

        if (new Vector2(horizontalInput, verticalInput) == Vector2.zero)
        {
            animator.SetBool("isRunning", false);
        }

        if (_controller.isGrounded)
        {
            moveDirection = new Vector3(horizontalInput, 0, verticalInput);
            moveDirection.Normalize();
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= currentSpeed;
            if (new Vector2(horizontalInput, verticalInput) != Vector2.zero)
            {
                if (isRunning)
                {
                    animator.SetBool("isRunning", true);
                }
                else if (isCrouching)
                {
                    animator.SetBool("isRunning", false);
                }
                else
                {
                    animator.SetBool("isRunning", false);
                }
            }
            if (_hasJumped)
            {
                moveDirection.y = jumpForce;
                _charOwner.CharacterStats.CurrentStamina -= jumpingStaminaReduce;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);
    }

}
