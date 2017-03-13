using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Vector2i))]
public class Vector2iDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        float inputWidth = position.width / 3f - 14f;

        EditorGUI.LabelField(new Rect(position.x - 1f, position.y, 12f, position.height), "X");
        EditorGUI.PropertyField(new Rect(position.x + 12f, position.y, inputWidth, position.height), property.FindPropertyRelative("x"), GUIContent.none);

        EditorGUI.LabelField(new Rect(position.x + inputWidth + 14f, position.y, 12f, position.height), "Y");
        EditorGUI.PropertyField(new Rect(position.x + inputWidth + 27f, position.y, inputWidth, position.height), property.FindPropertyRelative("y"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}
