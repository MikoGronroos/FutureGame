using UnityEngine;

public interface IInput
{

    Vector3 MovementInput();
    bool RunInput();
    bool InteractInput();
    void RollInput(bool value);
    bool HitInput();
    bool DefendInput();
    bool InventoryInput();

}