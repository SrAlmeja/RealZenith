using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PixelSubtitles : MonoBehaviour
{
    [SerializeField] DialogueSystemTrigger _dialogueSystemTrigger;
    [SerializeField] private GameObject _interactButtonUI;

    
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
        UseDialog();
    }

    private void UseDialog()
    {
        _dialogueSystemTrigger.OnUse();
    }
    
    
}
