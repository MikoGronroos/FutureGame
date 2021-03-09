using UnityEngine;
using TMPro;

public class InputButton : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI inputText;

    private string _keyCodeName;

    public void OnButtonPressed()
    {
        RefreshInputText("Press any key.");
        MessageSender.SendMessageToClientsWithContent("StartedRemappingEvent", _keyCodeName);
    }

    public void RefreshInputText(string text)
    {
        inputText.text = text;
    }

    public void SetKeyCodeName(string name)
    {
        _keyCodeName = name;
    }
}
