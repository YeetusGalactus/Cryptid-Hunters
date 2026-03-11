using UnityEngine;
using UnityEditor;

public class PlaneZFightFixer : EditorWindow
{
    float heightOffset = 0.001f; // small offset to prevent Z-fighting

    [MenuItem("Tools/Fix Plane Z-Fighting")]
    public static void ShowWindow() {
        GetWindow<PlaneZFightFixer>("Z-Fight Fixer");
    }

    void OnGUI() {
        GUILayout.Label("Adjust Plane Heights to Prevent Z-Fighting", EditorStyles.boldLabel);
        heightOffset = EditorGUILayout.FloatField("Height Offset", heightOffset);

        if (GUILayout.Button("Fix Selected Planes")) {
            FixSelectedPlanes();
        }
    }

    void FixSelectedPlanes() {
        var selectedPlanes = Selection.gameObjects;
        if (selectedPlanes.Length == 0) {
            Debug.LogWarning("No planes selected!");
            return;
        }

        for (int i = 0; i < selectedPlanes.Length; i++) {
            Undo.RecordObject(selectedPlanes[i].transform, "Z-Fight Fix");
            Vector3 pos = selectedPlanes[i].transform.position;
            pos.y += i * heightOffset;
            selectedPlanes[i].transform.position = pos;
        }

        Debug.Log("Adjusted " + selectedPlanes.Length + " planes to avoid Z-fighting.");
    }
}