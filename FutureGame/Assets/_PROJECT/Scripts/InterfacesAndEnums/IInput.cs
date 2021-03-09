using UnityEngine;

public interface IInput
{

    float HorizontalInput();
    float VerticalInput();
    bool RunInput();
    bool InteractInput();
    bool HitInput();
    bool DefendInput();
    bool InventoryInput();
    bool JumpInput();
    bool CrouchInput();
    bool PauseInput();
}