using UnityEngine;
using TMPro;

public class InputButton : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI inputText;

    private bool _pressedButton;
    private SettingsSystem _settingsSystem;
    private string _keyCodeName;

    private void Awake()
    {
        _settingsSystem = FindObjectOfType<SettingsSystem>();
    }

    public void OnButtonPressed()
    {
        RefreshInputText("Press any key.");
        _pressedButton = true;
    }

    public void RefreshInputText(string text)
    {
        inputText.text = text;
    }

    public void SetKeyCodeName(string name)
    {
        _keyCodeName = name;
    }

    private void Update()
    {
        if (_pressedButton)
        {
            var e = System.Enum.GetNames(typeof(KeyCode)).Length;
            for (int i = 0; i < e; i++)
            {
                if (Input.GetKey((KeyCode)i))
                {
                    _settingsSystem.AssignNewKeyCode((KeyCode)i, _keyCodeName);
                    RefreshInputText(((KeyCode)i).ToString());
                    _pressedButton = false;

                }
            }
        }
    }
}
