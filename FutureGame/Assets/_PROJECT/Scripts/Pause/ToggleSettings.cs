using UnityEngine.SceneManagement;

public static class ToggleSettings
{

    public static bool _isOpen;

    public static void ToggleSettingsMenu()
    {
        if (!_isOpen)
        {
            CursorVisibility.SetCursorVisible();
            SceneManager.LoadSceneAsync("SettingsScene", LoadSceneMode.Additive);
            _isOpen = true;
            return;
        }
        CursorVisibility.SetCursorHidden();
        SceneManager.UnloadSceneAsync("SettingsScene");
        _isOpen = false;
    }
}
