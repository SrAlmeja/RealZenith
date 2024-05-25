using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider))]
public class DialogueNewNpcs : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _dialogueMeshUI;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _speakerText;
    [SerializeField] private GameObject _interactButtonUI;
    
    // [Header("Animations")]
    // [SerializeField] private string _animationIdle;
    // [SerializeField] private string _animationTalk;
    // [SerializeField] private Animator _animatorController;
    
    
    
    public InputActionReference interactAction;
    public bool isActive;
    public bool isDone;
    
    public DialogueStruct thisDialogue;
    
    public UnityEvent eventAtFinish;
    public UnityEvent onStartDialogue;

    private string _currentText;
    private CapsuleCollider _collider;
    private bool _canInteract;
    private bool _dialogueStarted;

    private void OnEnable()
    {
        interactAction.action.Enable();
        interactAction.action.performed += Interact;
    }

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
          
        }
    
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (isDone)
        {
            _canInteract = false;
            return;
        }
        
        if (!isActive)
            return;
        

        if (other.CompareTag("Player"))
        {
            if (_dialogueStarted)
            {
                return;
            }
                
            _canInteract = true;
            EnableInteractUI(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(isDone) return;
        
        if (!isActive)
            return;
        
        if (other.CompareTag("Player"))
        {
            _canInteract = false;
            EnableInteractUI(false);
        }
    }

    private void SkipTextEffect()
    {
        _dialogueText.DOKill();
        _dialogueText.text = _currentText;
        OnFinishedDialogue();
        
    }
    
    public void ActiveDialogue()
    {
        isActive = true;
    }

    public void LaunchNpcConversation()
    {
        _dialogueStarted = true;
        EnableInteractUI(false);
        
        _currentText = thisDialogue.dialogues[0];
        
        _dialogueMeshUI.SetActive(true);
        _dialogueText.text = "";
        _speakerText.text = thisDialogue.npcName;
        _dialogueText.DOText(_currentText, _currentText.Length * 0.03f).SetEase(Ease.Linear).OnComplete(() =>
        {
             OnFinishedDialogue();
        });
        
        onStartDialogue.Invoke();
    }
    

    private void OnFinishedDialogue()
    {
        isActive = false;
        isDone = true;
        _collider.enabled = false;
    }
    
    private void DeactivateDialogue()
    {
        eventAtFinish.Invoke();
        _dialogueMeshUI.SetActive(false);
        gameObject.SetActive(false);
    }
    private void Interact(InputAction.CallbackContext context)
    {
        
        if(isDone) DeactivateDialogue();

        if (_canInteract)
        {
            if (_dialogueStarted)
            {
                SkipTextEffect();
            }
            else
            {
                LaunchNpcConversation();
            }
        }
        
        
    }
    
    private void EnableInteractUI(bool enable)
    {
        _interactButtonUI.SetActive(enable);
    }
}

[Serializable]
public struct DialogueStruct
{
    public string npcName;
    public string[] dialogues;
}

