using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReferenceToGUIDAttribute))]
public class ReferenceToGUIDDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        ReferenceToGUIDAttribute atr = (ReferenceToGUIDAttribute)attribute;

        Object obj = null;
        string GUID = string.Empty;

        if (prop.propertyType == SerializedPropertyType.String && !string.IsNullOrEmpty(prop.stringValue))
        {
            GUID = prop.stringValue;
            obj = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(GUID), atr.type);
        }

        obj = EditorGUI.ObjectField(new Rect(position.xMin, position.yMin, position.width, position.height), label, obj, atr.type, true);

        if (obj != null)
        {
            prop.stringValue = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(obj));
        }
        else
        {
            prop.stringValue = "";
        }
    }
}
