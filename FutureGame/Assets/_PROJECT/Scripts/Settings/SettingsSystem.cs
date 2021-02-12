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

}
