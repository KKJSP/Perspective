using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColourController))]
public class ColourControllerEditor : Editor
{
    int[] selected = new int[5];

    string[] options = new string[]
    {
        "Black", "ColourOne", "ColourTwo",
    };


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ColourController myScript = (ColourController)target;

        selected[0] = EditorGUILayout.Popup("Front", selected[0], options);
        selected[1] = EditorGUILayout.Popup("Back", selected[1], options);
        selected[2] = EditorGUILayout.Popup("Right", selected[2], options);
        selected[3] = EditorGUILayout.Popup("Left", selected[3], options);
        selected[4] = EditorGUILayout.Popup("Top", selected[4], options);

        if (GUILayout.Button("Save Changes"))
        {
            myScript.ApplyMaterials(selected);
        }

    }

}
