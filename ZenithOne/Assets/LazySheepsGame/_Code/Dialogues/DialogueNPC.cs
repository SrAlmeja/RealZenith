using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using DG.Tweening;
using Obvious.Soap;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueNPC : DialogueBase
{
    [Header("UI")]
    [SerializeField] private GameObject _dialogueMeshUI;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _speakerText;
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private GameObject _interactButtonUI;
    [SerializeField] private GameObject nextDialogImage;
    [SerializeField] private Transform arrowNextDialog;
    
    
    [Header("Input Events")]
    [SerializeField] private InputActionReference _interactAction;
    
    private string _currentText;
    private bool _isDialogueActive;
    private bool _canInteract;
    private bool _canContinue;

    #region unity methods
    
    protected override void Start()
    {
        base.Start();
        _dialogueMeshUI.SetActive(false);
        _interactAction.action.performed += Interact;

    }
    private void OnEnable()
    {
        _interactAction.action.Enable();
    }
    private void OnDisable()
    {
        _interactAction.action.Disable();
    }
    #endregion

    #region Dialogue Base
    
    public override void SendDialogue()
    {
        EnableInteractButton(false);
        base.SendDialogue();
    }

    public override void ContinueDialogue()
    {
        if (_isDialogueActive)
        {
            SkipTextEffect(_currentText);
            
        }
        else
        {
            base.ContinueDialogue();
        }
        
    }

    protected override void OnDialogueEnd()
    {
        base.OnDialogueEnd();
        _currentText = "";
        _dialogueText.text = "";
        _dialogueMeshUI.SetActive(false);
        
    }
    
    #endregion

    
    
    public void SetDialogueToNpc(DialogueInfoUI dialogueInfoUI)
    {
        _dialogueMeshUI.SetActive(true);
        _canContinue = dialogueInfoUI.CanContinue;  
        EnableCanContinue(_canContinue);
        DisplayTextEffect(dialogueInfoUI.Text);
        SetSpeaker(dialogueInfoUI.Speaker);
    }

   

    private void SetSpeaker(string speaker)
    {
        // Debug.Log("Speaker: ".SetColor("#41E8B8") + speaker);
        _speakerText.text = speaker;
    }
    private void DisplayTextEffect(string text)
    {
        _dialogueText.text = "";
        _currentText = text;
        // _dialogueText.text = _currentText;
        _isDialogueActive = true;
        _dialogueText.DOText(_currentText, _currentText.Length * 0.03f).SetEase(Ease.Linear).OnComplete(() =>
        {
           _isDialogueActive = false;
        });
    }
    private void EnableCanContinue(bool enable)
    {
        if (nextDialogImage == null || nextDialogImage.activeSelf == enable)
            return;
        
        nextDialogImage.SetActive(enable);
    }
    private void SkipTextEffect(string text)
    {
        Debug.Log("Skip Text Effect");
        _dialogueText.DOKill();
        _dialogueText.text = text;
        _isDialogueActive = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canInteract = true;
            EnableInteractButton(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canInteract = false;
            EnableInteractButton(false);
        }
    }
    
    private void EnableInteractButton(bool enable)
    {
        if (_interactButtonUI == null || _interactButtonUI.activeSelf == enable)
            return;
        
        _interactButtonUI.SetActive(enable);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (!_canInteract)
            return;
        
        // Debug.Log("Interact with NPC");
        if (String.IsNullOrEmpty(_currentText))
        {
            SendDialogue();
        }
        else
        {
            ContinueDialogue();
        }
    }

   
}
