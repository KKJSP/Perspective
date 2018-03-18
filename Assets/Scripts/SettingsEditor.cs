using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Settings))]
public class SettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Settings myScript = (Settings) target;
        if(GUILayout.Button("Save Changes"))
        {
            myScript.SaveChanges();
        }
    }
}