using UnityEngine;

public class Settings : MonoBehaviour
{

    [SerializeField] private InputSettings inputSettings;

    public InputSettings InputSettings { get { return inputSettings; } }

    #region Singleton

    private static Settings _instance;

    public static Settings Instance { get { return _instance; } }

    #endregion

    private void Awake()
    {
        _instance = this;
    }

}
