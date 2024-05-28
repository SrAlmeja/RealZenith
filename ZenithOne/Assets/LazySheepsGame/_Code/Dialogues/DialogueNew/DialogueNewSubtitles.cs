using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider))]

public class DialogueNewSubtitles : MonoBehaviour
{
    public bool isActive;
    public bool isDone;
    public DialogueStruct thisDialogue;
    
    public UnityEvent eventAtFinish;
    
    private CapsuleCollider _collider;
    
    
    private void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _collider.isTrigger = true;
        _collider.radius = 1.5f;
        
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if(isDone) return;
        
        if (!isActive)
            return;

        if (other.CompareTag("Player"))
        {
            LaunchSubtitles();
        }
        
    }
    
    public void ActiveDialogue()
    {
        isActive = true;
    }
    
    public void LaunchSubtitles()
    {
        Debug.Log(thisDialogue.dialogues[0]);
        PlayerSubtitlesUI.Instance.DisplayText(thisDialogue.dialogues[0]);
        eventAtFinish.Invoke();
        isActive = false;
        isDone = true;

    }
}
