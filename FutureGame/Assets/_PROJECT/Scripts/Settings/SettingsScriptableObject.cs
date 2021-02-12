using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Settings")]
public class SettingsScriptableObject : ScriptableObject
{

    public bool AmbientOcclusion = true;

    public Controls Controls;

}
