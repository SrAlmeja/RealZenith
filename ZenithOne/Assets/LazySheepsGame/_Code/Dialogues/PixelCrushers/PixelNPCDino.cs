using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PixelNPCDino : MonoBehaviour, IDialogueUI
{
    [SerializeField] DialogueSystemTrigger _dialogueSystemTrigger;
    [SerializeField] private GameObject _interactButtonUI;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI _speakerText;
    [SerializeField] private GameObject _dialogueMeshUI;



    
    [Header("Input Events")]
    [SerializeField] private InputActionReference _interactAction;

    private bool _canInteract;


    private void OnEnable()
    {
        _interactAction.action.Enable();
    }
    private void OnDisable()
    {
        _interactAction.action.Disable();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnableInteractButton(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnableInteractButton(false);
        }
    }
    
    private void EnableInteractButton(bool enable)
    {
        if (_interactButtonUI == null || _interactButtonUI.activeSelf == enable)
            return;
        
        _interactButtonUI.SetActive(enable);
    }
    void Start()
    {
        _interactAction.action.performed += Interact;
    }

    void Update()
    {
        
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (!_canInteract)
            return;
        
        Debug.Log("Interact with NPC");
        TriggerDialogue();
    }

    private void TriggerDialogue()
    {
        _dialogueSystemTrigger.Start();
        // _dialogueSystemTrigger.
    }


    public event EventHandler<SelectedResponseEventArgs> SelectedResponseHandler;
    public void Open()
    {
    }

    public void Close()
    {
    }

    public void ShowSubtitle(Subtitle subtitle)
    {
    }

    public void HideSubtitle(Subtitle subtitle)
    {
    }

    public void ShowResponses(Subtitle subtitle, Response[] responses, float timeout)
    {
    }

    public void HideResponses()
    {
    }

    public void ShowQTEIndicator(int index)
    {
    }

    public void HideQTEIndicator(int index)
    {
    }

    public void ShowAlert(string message, float duration)
    {
    }

    public void HideAlert()
    {
    }
}
