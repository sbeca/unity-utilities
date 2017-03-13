using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public static class MoveComponentContext
{
    private enum Destination
    {
        Top,
        Bottom
    };

    private const string kComponentArrayName = "m_Component";
    private const int kFirstComponentIndex = 1;

    [MenuItem("CONTEXT/Component/Move To Top")]
    private static void Top(MenuCommand command)
    {
        Move((Component)command.context, Destination.Top);
    }

    [MenuItem("CONTEXT/Component/Move To Bottom")]
    private static void Bottom(MenuCommand command)
    {
        Move((Component)command.context, Destination.Bottom);
    }

    private static void Move(Component target, Destination destination)
    {
        SerializedObject gameObject = new SerializedObject(target.gameObject);
        SerializedProperty componentArray = gameObject.FindProperty(kComponentArrayName);

        GameObject prefabRoot = PrefabUtility.FindPrefabRoot(target.gameObject);
        Object prefab = (prefabRoot == null ? null : PrefabUtility.GetPrefabObject(prefabRoot));
        bool doMove = true;
        if (prefab != null)
        {
            doMove = EditorUtility.DisplayDialog("Break Prefab Instance", "This action will break the prefab instance. Are you sure you wish to continue?", "Continue", "Cancel");
        }

        if (doMove)
        {
            int lastComponentIndex = componentArray.arraySize - 1;
            int targetIndex = destination == Destination.Top ? kFirstComponentIndex : lastComponentIndex;

            for (int index = kFirstComponentIndex; index <= lastComponentIndex; index++)
            {
                SerializedProperty iterator = componentArray.GetArrayElementAtIndex(index);
                iterator.Next(true);

                if (iterator.objectReferenceValue == target)
                {
                    gameObject.Update();
                    componentArray.MoveArrayElement(index, targetIndex);
                    gameObject.ApplyModifiedProperties();

                    if (prefab != null) PrefabUtility.DisconnectPrefabInstance(target.gameObject);
                    EditorUtility.SetDirty(target.gameObject);
                    EditorSceneManager.MarkSceneDirty(target.gameObject.scene);

                    break;
                }
            }
        }
    }
}
