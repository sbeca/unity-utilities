using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public static class CreateNewCustomEditor
{
    [MenuItem("Assets/Create/C# Custom Editor Script", false, 81)]
    private static void CreateCustomEditor()
    {
        UnityEngine.Object obj = Selection.activeObject;

        string dataPath = Application.dataPath.Substring(0, Application.dataPath.Length - 6);
        string assetPath = AssetDatabase.GetAssetPath(obj.GetInstanceID());
        assetPath = assetPath.Substring(6);
        string fullPath = dataPath + "Assets" + assetPath;

        string fileName = Path.GetFileName(fullPath);
        string assetDir = assetPath.Replace(fileName, "");

        if (!Directory.Exists(dataPath + "Assets" + assetDir + "Editor"))
        {
            AssetDatabase.CreateFolder("Assets" + assetDir.Substring(0, assetDir.Length - 1), "Editor");
        }

        string className = "";
        string namespaceName = "";
        string editorClassName = "";
        string[] lines = File.ReadAllLines(fullPath);
        foreach (string line in lines)
        {
            if (line.Contains("namespace "))
            {
                namespaceName = line.Replace("namespace", "");
                namespaceName = namespaceName.Replace("{", "");
                namespaceName = namespaceName.Replace(" ", "");
            }

            if (line.Contains("public ") && line.Contains("class ") && line.Contains("MonoBehaviour"))
            {
                className = line.Replace("public", "");
                className = className.Replace("class", "");
                int colon = className.IndexOf(":", StringComparison.CurrentCulture);
                className = className.Substring(0, colon);
                className = className.Trim();
                editorClassName = className + "Editor";
            }
        }

        if (className == "")
        {
            Debug.LogError("[CreateNewCustomEditor] Cannot find classname in " + fileName);
            return;
        }

        string indent = "";

        List<string> contents = new List<string>();
        contents.Add("using UnityEngine;");
        contents.Add("using UnityEditor;");
        contents.Add("");
        if (namespaceName != "")
        {
            indent = "    ";
            contents.Add("namespace " + namespaceName);
            contents.Add("{");
        }

        contents.Add(indent + "[CustomEditor(typeof(" + className + "))]");
        contents.Add(indent + "public class " + editorClassName + " : Editor");
        contents.Add(indent + "{");
        contents.Add(indent + "    void OnEnable()");
        contents.Add(indent + "    {");
        contents.Add(indent + "    }");
        contents.Add("");
        contents.Add(indent + "    public override void OnInspectorGUI()");
        contents.Add(indent + "    {");
        contents.Add(indent + "        DrawDefaultInspector();");
        contents.Add(indent + "    }");
        contents.Add("");
        contents.Add(indent + "    void OnSceneGUI()");
        contents.Add(indent + "    {");
        contents.Add(indent + "    }");
        contents.Add(indent + "}");

        if (namespaceName != "")
        {
            contents.Add("}");
        }

        string editorScriptPath = AssetDatabase.GenerateUniqueAssetPath("Assets" + assetDir + "Editor/" + editorClassName + ".cs");

        File.WriteAllLines(editorScriptPath, contents.ToArray());
        AssetDatabase.Refresh();

        Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(editorScriptPath);
    }

    [MenuItem("Assets/Create/C# Custom Editor Script", true, 81)]
    private static bool CreateInspectorValidator()
    {
        return Selection.activeObject is MonoScript;
    }
}
