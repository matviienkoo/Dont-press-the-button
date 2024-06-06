using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(AdministartorScript))]
public class EditorSystem : Editor 
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Удалить все"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}