using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausing : MonoBehaviour
{

    private bool isOpen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpen)
            {
                SceneManager.LoadSceneAsync("SettingsScene", LoadSceneMode.Additive);
                isOpen = true;
                return;
            }
            SceneManager.UnloadSceneAsync("SettingsScene");
            isOpen = false;
        }
    }
}
