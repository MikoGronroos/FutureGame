using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterStats))]
public class CharacterStatsEditor : Editor
{

    private float AmountToAdd;

    public override void OnInspectorGUI()
    {
        CharacterStats charStats = (CharacterStats)target;

        DrawDefaultInspector();

        GUILayout.Label("Stat Changer");

        AmountToAdd = EditorGUILayout.FloatField("Amount Of Change", AmountToAdd);

        if (GUILayout.Button("Change Health") && Application.isPlaying)
        {
            Debug.Log($"Adding {AmountToAdd} health!");
            CharacterOwner.Instance.CharacterStats.CurrentHealth += AmountToAdd;
        }

        if (GUILayout.Button("Kill") && Application.isPlaying)
        {
            Debug.Log($"Killed Player");
            CharacterOwner.Instance.CharacterStats.CurrentHealth = 0;
        }

    }
}
