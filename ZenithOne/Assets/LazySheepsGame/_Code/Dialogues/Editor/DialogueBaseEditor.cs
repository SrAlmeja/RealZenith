using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueBase), true)]
public class DialogueBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DialogueBase dialogueBase = (DialogueBase)target;
        if (GUILayout.Button("Send Dialogue"))
        {
            dialogueBase.SendDialogue();
        }

        if (GUILayout.Button("Continue Dialogue"))
        {
            dialogueBase.ContinueDialogue();
        }
    }

}
