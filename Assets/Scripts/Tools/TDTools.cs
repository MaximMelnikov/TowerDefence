using UnityEditor;
using UnityEngine;

public class TDTools : Editor
{
    public static SceneSwitcherWindow sceneSwicherWindow;
    [MenuItem("TD Tools/Scene Switcher")]
    private static void Init()
    {
        sceneSwicherWindow = (SceneSwitcherWindow)EditorWindow.GetWindow(typeof(SceneSwitcherWindow));
        sceneSwicherWindow.titleContent = new GUIContent("Scene Switcher");
    }
}