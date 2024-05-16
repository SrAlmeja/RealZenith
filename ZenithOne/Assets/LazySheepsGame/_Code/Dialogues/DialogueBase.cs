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
    
    [Header("NPC info")]
    [SerializeField] private CHARACTER _character;

    
    [Header("Container")]
    [SerializeField] private DialogueContainer _dialogueContainer;
    
    
    public CHARACTER Character => _character;
    private TextAsset _inkJSON;
    public TextAsset InkJSON => _inkJSON;
    // public DialogueContainer DialogueContainer => _dialogueContainer;
    
    public InksContainers CurrentInkContainer => _currentInkContainer;

    public bool HasDialogueToTrigger
    {
        get
        {
            if (!String.IsNullOrEmpty(_currentInkContainer._nextDialogueTriggered))
            {
                return true;
            }

            return false;
        }
    }
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
        Debug.Log("Dialogue Ended from ".SetColor("#41E8B8") + gameObject.name);
        if (_currentInkContainer != null)
        {
            _currentInkContainer.IsDialogueEnd = true;
            
            if (HasDialogueToTrigger)
            {
                string nextDialogue = _currentInkContainer._nextDialogueTriggered;
                CHARACTER character = _currentInkContainer.CharacterToTrigger;

                _currentInkContainer.OnDialogueEnd?.Invoke(nextDialogue, character);
            }
            
        }

       
    }
    
    public TextAsset GetInkJSON()
    {
        DialogueSection dialogueSection = _dialogueContainer.DialogueSections.FirstOrDefault(x => x.IsSectionEnd == false);
        
        if(dialogueSection == null)
        {
            Debug.LogError("Dialogue Section is null");
            return null;
        }
        _currentInkContainer = dialogueSection.InksContainers.FirstOrDefault(x => x.IsDialogueEnd == false);

        //if all the inks are done, then get the last ink
        if (_currentInkContainer == null)
        {
            _currentInkContainer = dialogueSection.InksContainers.FindLast(x => x.IsDialogueEnd == true);
        }
        else
        {
            _inkJSON = _currentInkContainer.InkJSON;
        }
        
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
        
        _onDialogueEnd.OnRaised -= OnDialogueEnd;
    }
    
}
