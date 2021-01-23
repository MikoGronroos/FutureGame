using UnityEngine;

public class CharacterMovement : MonoBehaviour, IMovement
{

    [SerializeField] private float currentSpeed;

    [SerializeField] private float walkingSpeed;

    [SerializeField] private bool running;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float runningStaminaReduce;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpingStaminaReduce;

    [SerializeField] private float gravity;

    private Vector3 moveDirection = Vector3.zero;

    private CharacterOwner _charOwner;
    private CharacterController _controller;
    private Rigidbody _rigidbody;

    //Input
    private float _horizontalInput;
    private float _verticalInput;

    private bool _hasJumped;

    private void Awake()
    {
        _charOwner = GetComponent<CharacterOwner>();
        _controller = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
        if (running)
        {
            _charOwner.CharacterStats.CurrentStamina -= runningStaminaReduce * Time.deltaTime;
            currentSpeed = runningSpeed;
        }
        else
        {
            currentSpeed = walkingSpeed;
        }
        Movement(_horizontalInput, _verticalInput);
    }

    private void HandleInput()
    {
        _horizontalInput = _charOwner.Input.HorizontalInput();
        _verticalInput = _charOwner.Input.VerticalInput();
        running = _charOwner.Input.RunInput();
        _hasJumped = _charOwner.Input.JumpInput();
    }

    public void Movement(float horizontalInput, float verticalInput)
    {
        if (_controller.isGrounded)
        {
            moveDirection = new Vector3(horizontalInput, 0, verticalInput);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= currentSpeed;
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
