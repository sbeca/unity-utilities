using UnityEngine;
using System.Diagnostics;
using System.IO;

using Debug = UnityEngine.Debug;

public static class OperatingSystemUtils
{
    public static string GetUsersHomeFolder()
    {
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        int index = Application.persistentDataPath.IndexOf("/Library/Application Support/");
        if (index > 0)
        {
            return Application.persistentDataPath.Substring(0, index);
        }
        else
        {
            Debug.LogError("[OperatingSystemUtils] Couldn't determine user's home folder");
            return null;
        }
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        int index = Mathf.Max(Application.persistentDataPath.IndexOf("\\AppData\\LocalLow\\"), Application.persistentDataPath.IndexOf("/AppData/LocalLow/")); // Who knows which direction the slashes will be in the path that Unity gives us... :(
        if (index > 0)
        {
            return Application.persistentDataPath.Substring(0, index);
        }
        else
        {
            Debug.LogError("[OperatingSystemUtils] Couldn't determine user's home folder");
            return null;
        }
#else
        Debug.LogError("[OperatingSystemUtils] Unsupported platform for getting the user's home folder");
        return null;
#endif
    }

    public static string GetUnityLogPath()
    {
#if UNITY_EDITOR_OSX
        string logPath = GetUsersHomeFolder();
        if (!string.IsNullOrEmpty(logPath))
        {
            logPath += "/Library/Logs/Unity/Editor.log";
        }
#elif UNITY_STANDALONE_OSX && !UNITY_EDITOR
        string logPath = GetUsersHomeFolder();
        if (!string.IsNullOrEmpty(logPath))
        {
            logPath += "/Library/Logs/Unity/Player.log";
        }
#elif UNITY_EDITOR_WIN
        string logPath = GetUsersHomeFolder();
        if (!string.IsNullOrEmpty(logPath))
        {
            logPath += "\\AppData\\Local\\Unity\\Editor\\Editor.log";
        }
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
        string logPath = Application.dataPath + "\\output_log.txt";
        if (!File.Exists(logPath))
        {
            logPath = Application.persistentDataPath + "\\output_log.txt";;
        }
#else
        string logPath = null;
        Debug.LogError("[OperatingSystemUtils] Unsupported platform for getting the Unity log");
        return null;
#endif

        if (!string.IsNullOrEmpty(logPath) && File.Exists(logPath))
        {
            return logPath;
        }
        else
        {
            Debug.LogError("[OperatingSystemUtils] Unable to find Unity log at: " + logPath);
            return null;
        }
    }

    public static string ZipFolder(string folderPath)
    {
        try
        {
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
            folderPath = folderPath.Replace("\\", "/");
            string zipFilePath = folderPath + ".zip";
            Process process = Process.Start("zip", "-rj \"" + zipFilePath + "\" \"" + folderPath + "\"");
            process.WaitForExit();
            return zipFilePath;
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            folderPath = folderPath.Replace("/", "\\");
            string zipFilePath = folderPath + ".zip";
            string zipScriptPath = Application.persistentDataPath + "/zip.vbs";
            if (File.Exists(zipScriptPath))
            {
                File.Delete(zipScriptPath);
            }
            using (StreamWriter file = new StreamWriter(zipScriptPath))
            {
                file.WriteLine("Set objArgs = WScript.Arguments");
                file.WriteLine("InputFolder = objArgs(0)");
                file.WriteLine("ZipFile = objArgs(1)");
                file.WriteLine("CreateObject(\"Scripting.FileSystemObject\").CreateTextFile(ZipFile, True).Write \"PK\" & Chr(5) & Chr(6) & String(18, vbNullChar)");
                file.WriteLine("Set objShell = CreateObject(\"Shell.Application\")");
                file.WriteLine("Set source = objShell.NameSpace(InputFolder).Items");
                file.WriteLine("objShell.NameSpace(ZipFile).CopyHere(source)");
                file.WriteLine("wScript.Sleep 2000");
            }
            Process process = Process.Start("CScript", "\"" + zipScriptPath + "\" \"" + folderPath + "\" \"" + folderPath + ".zip\"");
            process.WaitForExit();
            File.Delete(zipScriptPath);
            return zipFilePath;
#else
            Debug.LogError("[OperatingSystemUtils] Unsupported platform for zipping");
            return null;
#endif
        }
        catch (System.Exception e)
        {
            Debug.LogError("[LevelLoader] ZipFolder exception: " + e.ToString());
            return null;
        }
    }

    public static bool OpenFileExplorerWithSelected(string pathOfSelected)
    {
        try
        {
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            Process.Start("open", "-R \"" + pathOfSelected + "\"");
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            Process.Start("explorer.exe", "/select,\"" + pathOfSelected + "\"");
#elif UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
            Process.Start("nautilus", "\"" + pathOfSelected + "\"");
#else
            Debug.LogError("[OperatingSystemUtils] Unsupported platform for opening of the file explorer");
            return false;
#endif
        }
        catch (System.Exception e)
        {
            Debug.LogError("[LevelLoader] OpenFileExplorerWithSelected exception: " + e.ToString());
            return false;
        }

        return true;
    }
}
