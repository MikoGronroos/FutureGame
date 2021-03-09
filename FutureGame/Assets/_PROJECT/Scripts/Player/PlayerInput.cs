using UnityEngine;

public class PlayerInput : MonoBehaviour, IInput
{

    public float HorizontalInput()
    {
        if (Input.GetKey(Settings.Instance.InputSettings.GetKeyCode("StrafeLeft")))
        {
            return -1;
        }else if (Input.GetKey(Settings.Instance.InputSettings.GetKeyCode("StrafeRight")))
        {
            return 1;
        }
        return 0;
    }

    public float VerticalInput()
    {
        if (Input.GetKey(Settings.Instance.InputSettings.GetKeyCode("WalkBackward")))
        {
            return -1;
        }
        else if (Input.GetKey(Settings.Instance.InputSettings.GetKeyCode("WalkForward")))
        {
            return 1;
        }
        return 0;
    }

    public bool RunInput()
    {
        return Input.GetKey(Settings.Instance.InputSettings.GetKeyCode("Run"));
    }

    public bool PauseInput()
    {
        return Input.GetKeyDown(Settings.Instance.InputSettings.GetKeyCode("Pause"));
    }

    public bool InteractInput()
    {
        return Input.GetKeyDown(Settings.Instance.InputSettings.GetKeyCode("Interact"));
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
        return Input.GetKeyDown(Settings.Instance.InputSettings.GetKeyCode("Jump"));
    }
    
    public bool CrouchInput()
    {
        return Input.GetKey(Settings.Instance.InputSettings.GetKeyCode("Crouch"));
    }

}
