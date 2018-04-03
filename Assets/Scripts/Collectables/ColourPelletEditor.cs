using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColourPellet))]
public class ColourPelletEditor : Editor
{
    int selected;

    string[] options = new string[]
    {
        "Black", "ColourOne", "ColourTwo",
    };


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ColourPellet myScript = (ColourPellet)target;

        selected = EditorGUILayout.Popup("Changes Colour to : ", selected, options);

        if (GUILayout.Button("Save Changes"))
        {
            myScript.ApplyMaterials(selected);
        }

    }
}
