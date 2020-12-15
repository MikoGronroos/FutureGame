using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterStats))]
public class CharacterStatsEditor : Editor
{

    private float _removalAmount;

    public override void OnInspectorGUI()
    {
        CharacterStats charStats = (CharacterStats)target;

        DrawDefaultInspector();

        GUILayout.Label("Stat Removal");

        _removalAmount = EditorGUILayout.FloatField("Amount To Remove", _removalAmount);

        if (GUILayout.Button("Remove Health") && Application.isPlaying)
        {
            Debug.Log($"Removing {_removalAmount} health!");
            if (CharacterOwner.Instance.CharacterStats.CurrentHealth - _removalAmount < 0)
            {
                _removalAmount = CharacterOwner.Instance.CharacterStats.CurrentHealth;
            }
            CharacterOwner.Instance.CharacterStats.ReduceHealth(_removalAmount);
        }
    }
}
