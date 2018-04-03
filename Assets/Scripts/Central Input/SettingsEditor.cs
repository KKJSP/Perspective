using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Settings))]
public class SettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Settings myScript = (Settings)target;

        DrawDefaultInspector();

        if(GUILayout.Button("Save Changes"))
        {
            myScript.SaveChanges();
        }
    }
}