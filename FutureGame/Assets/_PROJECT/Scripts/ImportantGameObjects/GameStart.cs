using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    private void Awake()
    {
        Debug.Log("Started Loading Managers...");
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        if (!scene.isLoaded)
        {
            SceneManager.LoadSceneAsync("ManagerScene", LoadSceneMode.Additive).completed += OnManagersLoaded;
        }
    }

    private void OnManagersLoaded(AsyncOperation data)
    {

        Debug.Log($"{data} is loaded!");

    }
}
