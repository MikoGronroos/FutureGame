using UnityEngine;

public class CharacterMovement : MonoBehaviour, IMovement
{

    [SerializeField] private float currentSpeed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float _rollingForce;

    [SerializeField] private bool running;

    private CharacterOwner _charOwner;
    private Rigidbody _rigidbody;
    private Camera _camera;

    private float _turnSmoothVelocity;

    //Input
    private Vector3 _movementInput;
    [SerializeField] private bool _grounded;
    private bool _rolling;

    private void Awake()
    {
        _charOwner = GetComponent<CharacterOwner>();
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void Update()
    {
        HandleInput();
        currentSpeed = running ? runningSpeed : walkingSpeed;
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        Roll();
    }

    private void HandleInput()
    {
        _movementInput = _charOwner.Input.MovementInput();
        _grounded = _charOwner.GroundCheck.Grounded();
        running = _charOwner.Input.RunInput();
        _charOwner.Input.RollInput(_rolling);
    }

    public void MoveCharacter()
    {
        if (!_grounded || _rolling) return;

        float targetAngle = Mathf.Atan2(_movementInput.x, _movementInput.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (_movementInput.magnitude >= 0.1f)
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _rigidbody.velocity = moveDir.normalized * currentSpeed;
        }
    }

    private void Roll()
    {
        if (_rolling)
        {
            _rigidbody.AddForce(transform.forward * _rollingForce, ForceMode.Force);
            _rolling = false;
        }
    }
}
