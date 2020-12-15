using UnityEngine;

public class CodeLockButton : MonoBehaviour, IInteractable
{

    [SerializeField] private int buttonNumber;

    private CodeLockMain _lockMain;

    private void Awake()
    {
        _lockMain = GetComponentInParent<CodeLockMain>();
    }

    public void Interact()
    {
        _lockMain.AddNumberToCode(buttonNumber);
    }
}
