using UnityEngine;

public class PlayerInput : MonoBehaviour, IInput
{

    private CharacterOwner _charOwner;


    private void Awake()
    {
        _charOwner = GetComponent<CharacterOwner>();
    }

    public float HorizontalInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public float VerticalInput()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public bool RunInput()
    {
        return Input.GetButton("Run");
    }

    public bool InteractInput()
    {
        return Input.GetKeyDown(Settings.Instance.InputSettings.interactInput);
    }

    public bool HitInput()
    {
        return Input.GetButtonDown("Fire1");
    }

    public bool DefendInput()
    {
        return Input.GetButtonDown("Fire2");
    }

    public bool InventoryInput()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }

    public bool JumpInput()
    {
        return Input.GetButtonDown("Jump");
    }
}
