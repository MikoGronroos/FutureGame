using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(Resume);
        settingsButton.onClick.AddListener(Settings);
        quitButton.onClick.AddListener(Quit);
    }

    private void Resume()
    {

    }

    private void Settings()
    {
        ToggleSettings.ToggleSettingsMenu();
    }

    private void Quit()
    {
        Application.Quit();
    }

}
