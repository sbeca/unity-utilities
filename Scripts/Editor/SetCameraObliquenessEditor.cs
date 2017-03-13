using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetCameraObliqueness))]
public class SetCameraObliquenessEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SetCameraObliqueness myScript = (SetCameraObliqueness)target;
        if (GUILayout.Button("Set Obliqueness Now"))
        {
            myScript.SetObliqueness();
        }
    }
}
