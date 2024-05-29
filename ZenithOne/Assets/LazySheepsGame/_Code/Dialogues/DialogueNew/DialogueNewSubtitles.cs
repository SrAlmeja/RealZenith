using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

// [RequireComponent(typeof(CapsuleCollider))]

public class DialogueNewSubtitles : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    public bool isActive;
    public bool isDone;
    public bool needsArrow;

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

        if (needsArrow)
        {
            arrow.transform.DOJump(arrow.transform.position, 0.3f, 1, 0.5f).SetLoops(-1);
            EnableArrow(false);
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
        EnableArrow(true);
        
    }
    
    public void LaunchSubtitles()
    {
        if(isDone) return;
        if(!isActive) return;
        
        EnableArrow(false);
        // Debug.Log(thisDialogue.dialogueText);
        PlayerSubtitlesUI.Instance.DisplayText(thisDialogue.dialogueText);
        isActive = false;
        isDone = true;
        
        DeactivateDialogue();

    }
    
    private void DeactivateDialogue()
    {
        eventAtFinish.Invoke();
        
        this.enabled = false;

    }
    
    private void EnableArrow(bool enable)
    { 
        if(arrow != null)
            arrow.SetActive(enable);
    }
}
