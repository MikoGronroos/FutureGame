using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Input")]
public class Controls : ScriptableObject
{

    public float Sensitivity;
    public KeyCode InteractInput;
    public KeyCode WalkForwardInput;
    public KeyCode WalkBackwardInput;
    public KeyCode StrafeLeftInput;
    public KeyCode StrafeRightInput;

}
