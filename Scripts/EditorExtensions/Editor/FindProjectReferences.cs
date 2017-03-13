using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public static class FindProject
{
#if UNITY_EDITOR_OSX
    [MenuItem("Assets/Find References In Project", false, 2000)]
    private static void FindProjectReferences()
    {
        if (Selection.activeObject == null)
        {
            Debug.LogError("No asset currently selected");
            return;
        }

        string appDataPath = Application.dataPath;
        string selectedAssetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        string guid = AssetDatabase.AssetPathToGUID(selectedAssetPath);

        if (string.IsNullOrEmpty(selectedAssetPath) || string.IsNullOrEmpty(guid))
        {
            Debug.LogError("No asset currently selected");
            return;
        }

        List<string> references = FindProjectReferences(guid);

        if (selectedAssetPath.EndsWith(".cs"))
        {
            List<string> classNames = new List<string>();
            string[] lines = File.ReadAllLines(selectedAssetPath);
            foreach (string line in lines)
            {
                if (line.Contains("class"))
                {
                    // This is pretty bad. Is there a way to get all the possible class "modifiers"?
                    string className = line.Replace("class", "").Replace("{", "").Replace("public", "").Replace("protected", "").Replace("private", "").Replace("internal", "").Replace("static", "").Replace("abstract", "").Replace("partial", "").Replace("sealed", "");
                    if (className.Contains(":"))
                    {
                        className = className.Substring(0, className.IndexOf(":"));
                    }
                    className = className.Trim();

                    if (className.Contains(" "))
                    {
                        Debug.LogError("Was not able to parse the following class declaration (Scott missed somthing):\n" + line);
                        continue;
                    }

                    if (!classNames.Contains(className))
                        classNames.Add(className);
                }
            }

            foreach (string className in classNames)
            {
                List<string> classReferences = FindProjectReferences(className);
                foreach (string classReference in classReferences)
                {
                    if (classReference.EndsWith(".cs") && !references.Contains(classReference))
                        references.Add(classReference);
                }
            }
        }

        // Don't care about the meta file of whatever we have selected or what we have selected
        references.Remove(selectedAssetPath);
        references.Remove(selectedAssetPath + ".meta");

        string output = "";
        foreach (var file in references)
        {
            output += file + "\n";
            Debug.Log(file, AssetDatabase.LoadMainAssetAtPath(file));
        }

        Debug.Log(references.Count + " references found for object " + Selection.activeObject.name + "\n\n" + output);
    }

    private static List<string> FindProjectReferences(string searchString)
    {
        string appDataPath = Application.dataPath;
        List<string> references = new List<string>();

        var psi = new System.Diagnostics.ProcessStartInfo();
        psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
        psi.FileName = "/usr/bin/mdfind";
        psi.Arguments = "-onlyin " + Application.dataPath + " " + searchString;
        psi.UseShellExecute = false;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo = psi;

        process.OutputDataReceived += (sender, e) =>
        {
            if (string.IsNullOrEmpty(e.Data))
                return;

            string relativePath = "Assets" + e.Data.Replace(appDataPath, "");
            if (!references.Contains(relativePath))
                references.Add(relativePath);
        };
        process.ErrorDataReceived += (sender, e) =>
        {
            if (string.IsNullOrEmpty(e.Data))
                return;

            Debug.LogError(e.Data);
        };
        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        process.WaitForExit(10000);

        return references;
    }
#endif
}
