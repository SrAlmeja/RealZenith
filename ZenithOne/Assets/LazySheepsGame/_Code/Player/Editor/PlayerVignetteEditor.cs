using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerVignette))]
public class PlayerVignetteEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlayerVignette playerVignette = (PlayerVignette) target;
        if (GUILayout.Button("Fade In"))
        {
            playerVignette.DoFadeIn();
        }
        if (GUILayout.Button("Fade Out"))
        {
            playerVignette.DoFadeOut();
        }
    }
}
