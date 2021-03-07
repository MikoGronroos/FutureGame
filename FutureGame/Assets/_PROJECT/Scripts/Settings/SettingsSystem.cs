using System;
using UnityEngine;

public class SettingsSystem : MonoBehaviour
{

    [SerializeField] private SettingsScriptableObject currentSettings;

    public static Action<SettingsScriptableObject> OnSettingSystemAwake;

    private void Start()
    {
        OnSettingSystemAwake?.Invoke(currentSettings);
    }

    public void AssignNewKeyCode(KeyCode keyCode, string name)
    {
        currentSettings.Controls.AssignKeyCode(name, keyCode);
    }

    public void SaveSettings()
    {
        Debug.Log("Saved Settings");
    }

    public void ResetSettings()
    {
        Debug.Log("Reset Settings");
    }

}
