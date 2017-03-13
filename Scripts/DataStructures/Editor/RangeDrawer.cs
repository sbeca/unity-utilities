using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Range))]
public class RangeDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float labelWidth = EditorGUIUtility.labelWidth;
        float fieldsWidth = position.width - labelWidth;

        Rect labelRect = new Rect(position.xMin, position.yMin, labelWidth, position.height);
        Rect fromLabelRect = new Rect(position.xMin + labelWidth, position.yMin, fieldsWidth / 4f, position.height);
        Rect fromRect = new Rect(position.xMin + labelWidth + fieldsWidth / 4f, position.yMin, fieldsWidth / 4f, position.height);
        Rect toLabelRect = new Rect(position.xMin + labelWidth + fieldsWidth / 4f * 2f, position.yMin, fieldsWidth / 4f, position.height);
        Rect toRect = new Rect(position.xMin + labelWidth + fieldsWidth / 4f * 3f, position.yMin, fieldsWidth / 4f, position.height);

        GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;

        EditorGUI.LabelField(labelRect, label);
        EditorGUI.LabelField(fromLabelRect, "From:", centeredStyle);
        EditorGUI.PropertyField(fromRect, property.FindPropertyRelative("From"), GUIContent.none);
        EditorGUI.LabelField(toLabelRect, "To:", centeredStyle);
        EditorGUI.PropertyField(toRect, property.FindPropertyRelative("To"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}
