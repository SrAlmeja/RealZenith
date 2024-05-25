using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider))]
public class DialogueNew : MonoBehaviour
{
    public bool isActive;
    public bool isDone;
    public bool triggerButton;
    public DialogueStruct thisDialogue;
    // Dialoguie

    public UnityEvent eventAtFinish;

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
            return;

        if (!triggerButton)
            LaunchConversation();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isActive)
            return;

        if (triggerButton)
        {
            if (Input.GetKeyDown(KeyCode.Return))
             LaunchConversation();
        }
    }

    public void ActiveDialogue()
    {
        isActive = true;
    }

    public void LaunchConversation()
    {
        // Dialogo visual
        Debug.Log(thisDialogue.dialogues[0]);
        eventAtFinish.Invoke();
        isActive = false;
    }
}

[System.Serializable]
public struct DialogueStruct
{
    public string[] dialogues;
}

