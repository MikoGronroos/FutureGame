using UnityEngine;

public class CameraLook : MonoBehaviour
{

    [SerializeField] private float sensitivity;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    private float rotationX;
    private float rotationY;

    public float MinimumY = -60F;
    public float MaximumY = 60F;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        FollowPlayer();
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;

        rotationY = Mathf.Clamp(rotationY, MinimumY, MaximumY);

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }

    private void FollowPlayer()
    {
        transform.position = new Vector3(player.position.x, player.position.y + (transform.lossyScale.y / 2), player.position.z);
    }

}
