using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{

    [SerializeField] private Button returnFromSettingsButton;

    [SerializeField] private Button videoSettingsButton;
    [SerializeField] private Button controlSettingsButton;

    [SerializeField] private GameObject videoSettingsPanel;
    [SerializeField] private GameObject controlSettingsPanel;

    [SerializeField] private Button saveSettingsButton;
    [SerializeField] private Button resetSettingsButton;

    [Header("---VideoSettings---")]
    [SerializeField] private Toggle ambientOcclusion;

    [Header("---ControlSettings---")]
    [SerializeField] private TMP_InputField sensitivityField;

    [SerializeField] private KeyCodeButton[] keyCodeButtons;

    private SettingsSystem _settingsSystem;

    private void Awake()
    {
        _settingsSystem = FindObjectOfType<SettingsSystem>();
        videoSettingsButton.onClick.AddListener(ToggleVideoSettingsUI);
        controlSettingsButton.onClick.AddListener(ToggleControlSettingsUI);
        saveSettingsButton.onClick.AddListener(SaveSettings);
        resetSettingsButton.onClick.AddListener(ResetSettings);
        returnFromSettingsButton.onClick.AddListener(Return);
    }

    #region OnEnable & OnDisable

    private void OnEnable()
    {
        SettingsSystem.OnSettingSystemAwake += OnSettingSystemAwakeListener;
    }

    private void OnDisable()
    {
        SettingsSystem.OnSettingSystemAwake -= OnSettingSystemAwakeListener;
    }

    #endregion

    private void OnSettingSystemAwakeListener(SettingsScriptableObject data)
    {
        InitializeVideoSettingsUI(data);
        InitializeControlSettingsUI(data.Controls);
    }

    #region InitializeStuff

    private void InitializeVideoSettingsUI(SettingsScriptableObject data)
    {
        ambientOcclusion.isOn = data.AmbientOcclusion;
    }

    private void InitializeControlSettingsUI(Controls data)
    {
        sensitivityField.placeholder.GetComponent<TextMeshProUGUI>().text = data.Sensitivity.ToString();
        SetKeyCodeNames();
        RefreshAllKeyCodeButtons(data);
    }

    #endregion

    #region RefreshKeyCodes

    public void RefreshButtonText(string name, string keycode)
    {
        foreach (KeyCodeButton button in keyCodeButtons)
        {
            if (button.KeyCodeName == name)
            {
                button.ThisKeyCodeButton.RefreshInputText(keycode);
                return;
            }
        }
    }

    private void RefreshAllKeyCodeButtons(Controls data)
    {
        foreach (CustomKeyCode keycode in data.GetKeyCodes())
        {
            RefreshButtonText(keycode.KeyCodeName, keycode.PositiveKeyCode.ToString());
        }
    }

    private void SetKeyCodeNames()
    {
        foreach (KeyCodeButton button in keyCodeButtons)
        {
            button.ThisKeyCodeButton.SetKeyCodeName(button.KeyCodeName);
        }
    }
    #endregion

    private void ToggleVideoSettingsUI()
    {
        videoSettingsPanel.SetActive(true);
        controlSettingsPanel.SetActive(false);
    }

    private void ToggleControlSettingsUI()
    {
        videoSettingsPanel.SetActive(false);
        controlSettingsPanel.SetActive(true);
    }

    private void SaveSettings()
    {
        _settingsSystem.SaveSettings();
    }

    private void ResetSettings()
    {
        _settingsSystem.ResetSettings();
    }

    private void Return()
    {
        ToggleSettings.ToggleSettingsMenu();
    }

}

[System.Serializable]
public class KeyCodeButton
{
    public string KeyCodeName;
    public InputButton ThisKeyCodeButton;

}