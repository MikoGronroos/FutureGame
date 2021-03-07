using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausing : MonoBehaviour
{

    private bool _isOpen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (!_isOpen)
        {
            CursorVisibility.SetCursorVisible();
            SceneManager.LoadSceneAsync("PauseMenuUI", LoadSceneMode.Additive);
            _isOpen = true;
            return;
        }
        CursorVisibility.SetCursorHidden();
        SceneManager.UnloadSceneAsync("PauseMenuUI");
        _isOpen = false;
    }
}
