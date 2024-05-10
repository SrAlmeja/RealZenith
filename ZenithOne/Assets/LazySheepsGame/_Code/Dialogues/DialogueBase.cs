using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using com.LazyGames;
using Obvious.Soap;
using UnityEngine;

public class DialogueBase : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private ScriptableEventDialogueBase _onDialogueSend;
    [SerializeField] private ScriptableEventNoParam _onConinueDialogue;
    [SerializeField] ScriptableEventNoParam _onDialogueEnd;

    
    [Header("Container")]
    [SerializeField] private DialogueContainer _dialogueContainer;
    
    
    private TextAsset _inkJSON;
    public TextAsset InkJSON => _inkJSON;
    // public DialogueContainer DialogueContainer => _dialogueContainer;
    
    public InksContainers CurrentInkContainer => _currentInkContainer;
    private InksContainers _currentInkContainer;
    
    protected virtual void Start()
    {
        ResetDialogue();
        _onDialogueEnd.OnRaised += OnDialogueEnd;
    }

    private void OnDisable()
    {
        ResetDialogue();
    }

    public virtual void SendDialogue()
    {
        _onDialogueSend.Raise(this);
    }
    
    public virtual void ContinueDialogue()
    {
        _onConinueDialogue.Raise();
    }

     protected virtual void OnDialogueEnd()
    {
        Debug.Log("Dialogue Ended".SetColor("#41E8B8"));
        if(_currentInkContainer != null)
            _currentInkContainer.IsDialogueEnd = true;
    }
    
    public TextAsset GetInkJSON()
    {
        DialogueSection dialogueSection = _dialogueContainer.DialogueSections.FirstOrDefault(x => x.IsSectionEnd != true);
        
        if(dialogueSection == null)
        {
            Debug.LogError("Dialogue Section is null");
            return null;
        }
        _currentInkContainer = dialogueSection.InksContainers.FirstOrDefault(x => x.IsDialogueEnd != true);
        _inkJSON = _currentInkContainer.InkJSON;
        
        
        
        return _inkJSON;
    }
    private void ResetDialogue()
    {
        _currentInkContainer = null;
        _inkJSON = null;
        foreach (var variableDialogueSection in _dialogueContainer.DialogueSections)
        {
            variableDialogueSection.IsSectionEnd = false;
            foreach (var variableInksContainers in variableDialogueSection.InksContainers)
            {
                variableInksContainers.IsDialogueEnd = false;
            }
            
        }
    }
    
}
