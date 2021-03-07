using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Input")]
public class Controls : ScriptableObject
{

    public float Sensitivity;

    [SerializeField] private CustomKeyCode[] keyCodes;

    public KeyCode GetKeyCode(string name)
    {
        CustomKeyCode customKeyCode = GetCustomKeyCode(name);

        if (customKeyCode != null)
        {
            return customKeyCode.PositiveKeyCode;
        }
        return KeyCode.None;
    }

    public void AssignKeyCode(string name, KeyCode keyCode)
    {
        CustomKeyCode customKeyCode = GetCustomKeyCode(name);

        if (customKeyCode != null)
        {
            customKeyCode.PositiveKeyCode = keyCode;
        }

    }

    public IEnumerable<CustomKeyCode> GetKeyCodes()
    {
        return keyCodes;
    }

    private CustomKeyCode GetCustomKeyCode(string name)
    {
        foreach (CustomKeyCode key in keyCodes)
        {
            if (key.KeyCodeName.Equals(name))
            {
                return key;
            }
        }
        return null;
    }

}

[System.Serializable]
public class CustomKeyCode
{
    public string KeyCodeName;
    public KeyCode PositiveKeyCode;
}