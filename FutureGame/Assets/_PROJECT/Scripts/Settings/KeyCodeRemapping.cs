using System;
using UnityEngine;

public class KeyCodeRemapping : MonoBehaviour
{

    private bool _isRemapping;

    private string _currentKeyCodeName;

    private SettingsSystem _settingsSystem;
    private SettingsUI _settingsUI;

    private void Awake()
    {
        _settingsSystem = FindObjectOfType<SettingsSystem>();
        _settingsUI = FindObjectOfType<SettingsUI>();
    }

    private void Start()
    {
        MessageReceiver.SubscrideToMessage("StartedRemappingEvent", StartRemapping);
    }

    private void OnDisable()
    {
        MessageReceiver.UnsubscribeToMessage("StartedRemappingEvent", StartRemapping);
    }

    private void StartRemapping(string name, string content)
    {
        _currentKeyCodeName = content;
        _isRemapping = true;
    }

    private void Update()
    {
        if (_isRemapping)
        {
            var e = Enum.GetNames(typeof(KeyCode)).Length;
            for (int i = 0; i < e; i++)
            {
                if (Input.GetKey((KeyCode)i))
                {
                    _settingsSystem.AssignNewKeyCode((KeyCode)i, _currentKeyCodeName);
                    _settingsUI.RefreshButtonText(_currentKeyCodeName, ((KeyCode)i).ToString());
                    _currentKeyCodeName = "";
                    _isRemapping = false;
                }
            }
        }
    }
}
