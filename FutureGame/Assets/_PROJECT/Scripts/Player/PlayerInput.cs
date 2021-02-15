using UnityEngine;

public class PlayerInput : MonoBehaviour, IInput
{

    public float HorizontalInput()
    {
        if (Input.GetKey(Settings.Instance.InputSettings.StrafeLeftInput))
        {
            return -1;
        }else if (Input.GetKey(Settings.Instance.InputSettings.StrafeRightInput))
        {
            return 1;
        }
        return 0;
    }

    public float VerticalInput()
    {
        if (Input.GetKey(Settings.Instance.InputSettings.WalkBackwardInput))
        {
            return -1;
        }
        else if (Input.GetKey(Settings.Instance.InputSettings.WalkForwardInput))
        {
            return 1;
        }
        return 0;
    }

    public bool RunInput()
    {
        return Input.GetKey(Settings.Instance.InputSettings.RunInput);
    }

    public bool InteractInput()
    {
        return Input.GetKeyDown(Settings.Instance.InputSettings.InteractInput);
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
        return Input.GetKeyDown(Settings.Instance.InputSettings.JumpInput);
    }
    
    public bool CrouchInput()
    {
        return Input.GetKey(Settings.Instance.InputSettings.CrouchInput);
    }

}
