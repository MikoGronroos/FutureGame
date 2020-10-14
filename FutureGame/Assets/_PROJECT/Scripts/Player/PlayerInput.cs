using UnityEngine;

public class PlayerInput : MonoBehaviour, IInput
{

    [SerializeField] private float _rollTiming;

    private CharacterOwner _charOwner;

    private float _rollInterval = 0;

    private void Awake()
    {
        _charOwner = GetComponent<CharacterOwner>();
    }

    public Vector3 MovementInput()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    public bool RunInput()
    {
        return Input.GetButton("Run");
    }

    public bool InteractInput()
    {
        return Input.GetButtonDown("Interact");
    }

    public void RollInput(bool value)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rollInterval += 1 * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (_rollInterval <= _rollTiming)
            {
                Debug.Log("Rolling");
                value = true;
                _rollInterval = 0;
            }
            _rollInterval = 0;
        }
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
}
