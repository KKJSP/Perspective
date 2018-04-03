using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColourControllerStair))]
public class ColourControllerStairEditor : Editor
{
    int[] selected = new int[4];

    string[] options = new string[]
    {
        "Black", "ColourOne", "ColourTwo",
    };


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ColourControllerStair myScript = (ColourControllerStair)target;

        selected[0] = EditorGUILayout.Popup("Steps", selected[0], options);
        selected[1] = EditorGUILayout.Popup("Flat", selected[1], options);
        selected[2] = EditorGUILayout.Popup("R2L", selected[2], options);
        selected[3] = EditorGUILayout.Popup("L2R", selected[3], options);


        if (GUILayout.Button("Save Changes"))
        {
            myScript.ApplyMaterials(selected);
        }

    }

}
