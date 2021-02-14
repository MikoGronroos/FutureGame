using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AiFieldOfView))]
public class FieldOfViewEditor : Editor
{

    private void OnSceneGUI()
    {
        AiFieldOfView aiFow = (AiFieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(aiFow.transform.position, Vector3.up, Vector3.forward, 360, aiFow.ViewRadius);
        Vector3 viewAngleA = aiFow.DirFromAngle(-aiFow.ViewAngle / 2, false);
        Vector3 viewAngleB = aiFow.DirFromAngle(aiFow.ViewAngle / 2, false);

        Handles.DrawLine(aiFow.transform.position, aiFow.transform.position + viewAngleA * aiFow.ViewRadius);
        Handles.DrawLine(aiFow.transform.position, aiFow.transform.position + viewAngleB * aiFow.ViewRadius);
    }

}
