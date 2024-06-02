using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(CapsuleCollider))]
public class DialoguesNpcs : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private DialogueState _dialogueState = DialogueState.Inactive;
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private DialogueStruct dialogueStruct;
    
    [Header("UI")]
    [SerializeField] private GameObject _dialogueMeshUI;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _speakerText;
    [SerializeField] private GameObject _interactButtonUI;
    [SerializeField] private GameObject arrowDownText;
    
    [Header("Visuals")]
    [SerializeField] private GameObject npcTriangle;
    [SerializeField] private GameObject[] modelObjects;
    [SerializeField] private LayerSwitcher layerSwitcher;

    //[SerializeField] private bool _isLoopable;
    
    public UnityEvent eventAtFinish;
    public UnityEvent onStartDialogue;

    private bool _isActive;
    private bool _isDone;
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
        // _collider.radius = 1.5f;

       if(npcTriangle != null) npcTriangle.transform.DOJump(npcTriangle.transform.position, 0.3f, 1, 0.5f).SetLoops(-1);
        arrowDownText.transform.DOJump(arrowDownText.transform.position, 0.01f, 1, 0.5f).SetLoops(-1);
        HandleDialogueState();
        
    }

   

    private void OnTriggerStay(Collider other)
    {
        if (_dialogueState == DialogueState.Finished || _dialogueState == DialogueState.Inactive)
        {
            _canInteract = false;
            return;
        }
        

        if (other.CompareTag("Player"))
        {
            _canInteract = true;

            if (_dialogueState == DialogueState.Triggered)
            {
                EnableInteractUI(false);
                return;
            }
            EnableInteractUI(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // if(_isDone) return;
        // if (!_isActive)
            // return;
        
        
        if(_dialogueState == DialogueState.Finished || _dialogueState == DialogueState.Inactive)
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
        // _isActive = true;
        _dialogueState = DialogueState.Active;
        _currentText = "";
        _dialogueText.text = "";
        HandleDialogueState();
    }

    public void LaunchNpcConversation()
    {
        // _dialogueStarted = true;
        _dialogueState = DialogueState.Triggered;
        _currentText = dialogueStruct.dialogueText;
        _dialogueText.text = "";
        _speakerText.text = dialogueStruct.npcName;
        _dialogueText.DOText(_currentText, _currentText.Length * 0.03f).SetEase(Ease.Linear).OnComplete(() =>
        {
             OnFinishedDialogue();
        });
        
        onStartDialogue.Invoke();
        HandleDialogueState();
    }
    
    public void SwitchLayer(bool enable)
    {
        if (enable)
            layerSwitcher.OnSelected(null, modelObjects);
        else
            layerSwitcher.OnDeselected(null, modelObjects);
    }

    private void OnFinishedDialogue()
    { 
        _dialogueState = DialogueState.Finished;
    }
    
    private void DeactivateDialogue()
    {
        HandleDialogueState();
        interactAction.action.performed -= Interact;
        eventAtFinish.Invoke();
        this.enabled = false;
        
    }
    private void Interact(InputAction.CallbackContext context)
    {
        // if (_isDone)
        if(_dialogueState == DialogueState.Finished)
        {
            DeactivateDialogue();
        }

        if (_canInteract)
        {
            if (_dialogueState == DialogueState.Triggered)
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
    
    public void HandleDialogueState()
    {
        switch (_dialogueState)
        {
            case DialogueState.Inactive:
                _dialogueMeshUI.SetActive(false);
               if(npcTriangle != null) npcTriangle.SetActive(false);
                EnableInteractUI(false);
                
                break;
            case DialogueState.Active:
                if(npcTriangle != null) npcTriangle.SetActive(true);
                SetSelectedMaterial();
                break;
            case DialogueState.Triggered:
                EnableInteractUI(false);
                SetDeselectedMaterial();
                _dialogueMeshUI.SetActive(true);
                
                if(npcTriangle != null) npcTriangle.SetActive(false);
                break;
            case DialogueState.Finished:
                _dialogueMeshUI.SetActive(false);
                _collider.enabled = false;
                if(npcTriangle != null) npcTriangle.SetActive(false);
                gameObject.SetActive(false);
                

                break;
        }
    }
    
    private void SetSelectedMaterial()
    {
        if(layerSwitcher == null) return;
        layerSwitcher.OnSelected(null, modelObjects);
    }
    private void SetDeselectedMaterial()
    {
        if(layerSwitcher == null) return;
        layerSwitcher.DeselectObjectsDefault(modelObjects);
    }
}

[Serializable]
public struct DialogueStruct
{
    public string npcName;
    public string dialogueText;
}

public enum DialogueState
{
    Inactive,
    Active,
    Triggered,
    Finished
}

