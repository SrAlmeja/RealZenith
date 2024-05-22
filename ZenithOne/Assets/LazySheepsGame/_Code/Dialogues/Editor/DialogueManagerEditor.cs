using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueManager))]
public class DialogueManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DialogueManager dialogueManager = (DialogueManager)target;

        if (dialogueManager.DialoguesInScene.Count == 0)
        {
            if (GUILayout.Button("Find Dialogues In Scene"))
            {
                dialogueManager.FindDialogueInScene();
            }
        }

    }
    
}
