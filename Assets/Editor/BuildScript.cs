using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    /* App Info */
    private const string APP_Name = "SelfStory";
    private const string KEYSTORE_PASSWORD = "neural2016";
    private const string KEYALIAS_PASSWORD = "neural2016";

    private const string BUILD_BASIC_PATH = "C:\\Build";
    private const string BUILD_ANDROID_PATH = BUILD_BASIC_PATH + "\\Android\\";
    private const string BUILD_IOS_PATH = BUILD_BASIC_PATH + "\\iOS\\";

    private const string KEY_ALIAS = "moii";

    private const string LIVE_APK_NAME = "Moii";
    private const string TEST_APK_NAME = "Moiitest";

    [MenuItem("Builder/Build/BuildAndroid")]
    public static void BuildAndroid()
    {
        string fileName = SetPlayerSettingForAndroid();

        BuildPlayerOptions buildOptions = new BuildPlayerOptions();

        buildOptions.locationPathName = BUILD_ANDROID_PATH + fileName;
        buildOptions.scenes = GetBuildSceneList();
        buildOptions.target = BuildTarget.Android;
        BuildPipeline.BuildPlayer(buildOptions);
    }

    [MenuItem("Builder/OpenBuildDirectory")]
    public static void OpenBuildDirectory()
    {
        OpenFileBroswer(Path.GetFullPath(BUILD_BASIC_PATH));
    }

    private static void OpenFileBroswer(string path)
    {
        bool OpenInsidesOfFolder = false;
        if (Directory.Exists(path))
        {
            OpenInsidesOfFolder = true;
        }

        string arguments = (OpenInsidesOfFolder ? "" : "-R") + path;
        try
        {
            System.Diagnostics.Process.Start("open", arguments);
        }
        catch (Exception ex)
        {
            Debug.Log("Failded to open path: " + ex.ToString());
        }
    }

    private static string[] GetBuildSceneList()
    {
        EditorBuildSettingsScene[] scenes = UnityEditor.EditorBuildSettings.scenes;

        List<string> listScenePath = new List<string>();

        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].enabled)
                listScenePath.Add(scenes[i].path);
        }

        return listScenePath.ToArray();
    }

    private static string SetPlayerSettingForAndroid()
    {
        PlayerSettings.Android.keystorePass = KEYSTORE_PASSWORD;
        PlayerSettings.Android.keyaliasPass = KEYALIAS_PASSWORD;
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;

        string fileName = string.Format("{0}_{1}.apk", APP_Name, PlayerSettings.bundleVersion);

        return fileName;
    }
}
