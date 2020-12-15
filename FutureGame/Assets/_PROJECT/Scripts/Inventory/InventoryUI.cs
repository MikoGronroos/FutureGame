using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private GameObject inventoryPanel;

    private bool invIsOpen;
    private CharacterOwner _charOwner;
    private CameraLook _cameraLook;
    private CharacterMovement _charMovement;

    private void Awake()
    {
        _charMovement = FindObjectOfType<CharacterMovement>();
        _charOwner = FindObjectOfType<CharacterOwner>();
        _cameraLook = FindObjectOfType<CameraLook>();
    }

    private void Update()
    {
        if (_charOwner.Input.InventoryInput())
        {
            if (invIsOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    private void OpenInventory()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _charMovement.enabled = false;
        _cameraLook.enabled = false;
        inventoryPanel.SetActive(true);
        invIsOpen = true;
    }

    private void CloseInventory()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _charMovement.enabled = true;
        _cameraLook.enabled = true;
        inventoryPanel.SetActive(false);
        invIsOpen = false;
    }

}
