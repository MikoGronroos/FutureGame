using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausing : MonoBehaviour
{

    [SerializeField] private MonoBehaviour[] OnPauseToggle;

    private bool _isOpen;

    private CharacterOwner _charOwner;

    private void Start()
    {
        _charOwner = CharacterOwner.Instance;
    }

    private void Update()
    {
        if (_charOwner.Input.PauseInput())
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
            foreach (var script in OnPauseToggle)
            {
                script.enabled = false;
            }
            _isOpen = true;
            return;
        }
        else if (_isOpen && !ToggleSettings._isOpen)
        {
            CursorVisibility.SetCursorHidden();
            SceneManager.UnloadSceneAsync("PauseMenuUI");
            foreach (var script in OnPauseToggle)
            {
                script.enabled = true;
            }
            _isOpen = false;
        }
    }
}
