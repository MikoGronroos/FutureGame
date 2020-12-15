using UnityEngine;

public class CameraLook : MonoBehaviour
{

    [SerializeField] private float sensitivity;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform player;
    [SerializeField] private float turnSmoothTime;

    private Transform target;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private float _turnSmoothVelocity;

    private void Awake()
    {
        target = FindObjectOfType<CharacterMovement>().transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CameraPosition();
        CameraRotation();
    }

    private void LateUpdate()
    {
        RotatePlayer();
    }

    private void CameraPosition()
    {
        transform.position = target.position + cameraOffset;
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

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
