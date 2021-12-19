using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Camera2D))]
public class CameraFollowEditor : Editor
{

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        Camera2D cf = (Camera2D)target;

        if (GUILayout.Button("Min Camera Position"))
        {
            cf.SetMinCamPosition();
        }

        if (GUILayout.Button("Max Camera Position"))
        {
            cf.SetMaxCamPosition();
        }
    }

}