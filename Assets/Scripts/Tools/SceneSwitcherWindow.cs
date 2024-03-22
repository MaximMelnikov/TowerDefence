using UnityEngine;
using UnityEditor;

public class SceneSwitcherWindow : EditorWindow
{
    private Vector2 _scrollPosition;

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, false, false);
        DrawScenes();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private void DrawScenes()
    {
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                if (GUILayout.Button(scene.path) && EditorApplication.SaveCurrentSceneIfUserWantsTo())
                {
                    EditorApplication.OpenScene(scene.path);
                }
            }
        }
    }

}
