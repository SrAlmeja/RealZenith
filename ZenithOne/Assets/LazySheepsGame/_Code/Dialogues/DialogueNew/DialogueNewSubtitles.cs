using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [RequireComponent(typeof(CapsuleCollider))]

public class DialogueNewSubtitles : MonoBehaviour
{
    public bool isActive;
    public bool isDone;

    public bool needsCollider;
    public DialogueStruct thisDialogue;
    
    public UnityEvent eventAtFinish;
    
    private CapsuleCollider _collider;
    
    
    private void Start()
    {
        if (needsCollider)
        {
            _collider = GetComponent<CapsuleCollider>();
            _collider.isTrigger = true;
            _collider.radius = 1.5f;
        }
        
        
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
        if(isDone) return;
        if(!isActive) return;
        
        Debug.Log(thisDialogue.dialogueText);
        PlayerSubtitlesUI.Instance.DisplayText(thisDialogue.dialogueText);
        eventAtFinish.Invoke();
        isActive = false;
        isDone = true;
        
        DeactivateDialogue();

    }
    
    private void DeactivateDialogue()
    {
        this.enabled = false;
    }
}
