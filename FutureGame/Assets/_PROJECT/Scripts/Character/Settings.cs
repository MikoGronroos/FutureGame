using UnityEngine;

public class Settings : MonoBehaviour
{

    [SerializeField] private Controls inputSettings;

    public Controls InputSettings { get { return inputSettings; } }

    #region Singleton

    private static Settings _instance;

    public static Settings Instance { get { return _instance; } }

    #endregion

    private void Awake()
    {
        _instance = this;
    }

}
