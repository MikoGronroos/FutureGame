using UnityEngine;

public class BuildingUI : MonoBehaviour
{

    [SerializeField] private bool isEnabled;
    [SerializeField] private GameObject buildingMenu;

    private bool _uiOpen;
    private CharacterOwner _charOwner;
    private CharacterMovement _charMovement;
    private CameraLook _cameraLook;

    private void Awake()
    {
        _charOwner = FindObjectOfType<CharacterOwner>();
        _charMovement = FindObjectOfType<CharacterMovement>();
        _cameraLook = FindObjectOfType<CameraLook>();
    }

    private void Update()
    {
        if (!isEnabled) return;

        if (_charOwner.Input.DefendInput())
        {
            if (_uiOpen)
            {
                buildingMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                _charMovement.enabled = true;
                _cameraLook.enabled = true;
                _uiOpen = false;
            }
            else if (!_uiOpen)
            {
                buildingMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _charMovement.enabled = false;
                _cameraLook.enabled = false;
                _uiOpen = true;
            }
        }
    }

    public void SetEnabledValue(bool value)
    {
        isEnabled = value;
    }

}
