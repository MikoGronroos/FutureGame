using UnityEngine;
using TMPro;

public class SceneLoader : MonoBehaviour
{

    #region Singleton

    private static SceneLoader _instance;

    public static SceneLoader Instance
    {
        get
        {
            return _instance;
        }
    }

    #endregion

    [SerializeField] private GameObject loadingScreen;

    private TextMeshProUGUI _loadingPercentageText;

    private AsyncOperation _currentOperation;

    private void Awake()
    {
        _instance = this;
    }

    public void LoadScene(AsyncOperation operation)
    {
        _currentOperation = operation;
        GameObject screen = Instantiate(loadingScreen);
        _loadingPercentageText = GameObject.FindWithTag("ProgressText").GetComponent<TextMeshProUGUI>();
        GameObject.FindWithTag("MainCanvas").SetActive(false);
    }
    
    void Update()
    {

        if (_currentOperation == null) return;

        float progressValue = Mathf.Clamp01(_currentOperation.progress / 0.9f);
        string valueLoaded = Mathf.Round(progressValue * 100) + "%";
        _loadingPercentageText.text = valueLoaded;
    }

}
