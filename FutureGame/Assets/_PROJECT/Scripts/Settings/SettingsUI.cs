using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{

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

    private void Awake()
    {
        videoSettingsButton.onClick.AddListener(ToggleVideoSettingsUI);
        controlSettingsButton.onClick.AddListener(ToggleControlSettingsUI);
        saveSettingsButton.onClick.AddListener(SaveSettings);
        resetSettingsButton.onClick.AddListener(ResetSettings);
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

    private void InitializeVideoSettingsUI(SettingsScriptableObject data)
    {
        ambientOcclusion.isOn = data.AmbientOcclusion;
    }

    private void InitializeControlSettingsUI(Controls data)
    {
        sensitivityField.placeholder.GetComponent<TextMeshProUGUI>().text = data.Sensitivity.ToString();
    }

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

    }

    private void ResetSettings()
    {

    }

}
