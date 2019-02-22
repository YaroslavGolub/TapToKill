using UnityEngine;
using UnityEditor;
using TapToKill;

[CustomEditor(typeof(DebugEvents))]
public class EventHelper : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        var _script = target as DebugEvents;

        GUILayout.Space(10);

        if(GUILayout.Button("GameStart")) {
            _script.StartGame();
        }
        if(GUILayout.Button("GameRestart")) {
            _script.RestartGame();
        }
        if(GUILayout.Button("GameOver")) {
            _script.GameOver();
        }

        GUILayout.Space(10);
        if(GUILayout.Button("CleanPrefs")) {
            PlayerPrefs.DeleteAll();
        }
    }
}
