using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;

    private void Awake()
    {
        playButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(ToggleSettingsMenu);
    }

    private void Start()
    {
        CursorVisibility.SetCursorVisible();
    }

    private void StartGame()
    {
        CursorVisibility.SetCursorHidden();
        SceneLoader.Instance.LoadScene(SceneManager.LoadSceneAsync(1));
    }

    private void ToggleSettingsMenu()
    {
        ToggleSettings.ToggleSettingsMenu();
    }


}
