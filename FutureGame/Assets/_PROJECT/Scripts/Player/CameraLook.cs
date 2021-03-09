using UnityEngine;

public class CameraLook : MonoBehaviour
{

    [SerializeField] private float sensitivity;

    [Tooltip("Select A Percentage To Which Sensitivity Drops From Current Sensitivity")]
    [Range(0,1)]
    [SerializeField] private float onAirSensitivityReduce;

    [SerializeField] private Transform player;
    [SerializeField] private float turnSmoothTime;
    [SerializeField] private Transform cameraParentTransform;
    [SerializeField] private Vector3 headOffset;

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private float _turnSmoothVelocity;
    private CharacterOwner _charOwner;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        _charOwner = CharacterOwner.Instance;
    }

    private void OnDisable()
    {
        UpdateCameraPosition();
    }

    private void Update()
    {
        UpdateCameraPosition();
        CameraRotation();
        RotatePlayer();
    }

    private float CurrentSensitivity()
    {
        return _charOwner.GroundCheck.Grounded() ? sensitivity : sensitivity * onAirSensitivityReduce;
    }

    private void UpdateCameraPosition()
    {
        if (transform == null || cameraParentTransform == null)
        {
            return;
        }
        transform.position = cameraParentTransform.position + headOffset;
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * CurrentSensitivity();
        float mouseY = Input.GetAxisRaw("Mouse Y") * CurrentSensitivity();

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70, 70);

        transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }

    private void RotatePlayer()
    {
        float angle = Mathf.SmoothDampAngle(player.eulerAngles.y, transform.eulerAngles.y, ref _turnSmoothVelocity, turnSmoothTime);
        player.rotation = Quaternion.Euler(0f, angle, 0f);
    }

}
