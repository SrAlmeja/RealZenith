using System.Collections;
using System.Collections.Generic;
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
    // [SerializeField] private DialogueContainer _dialogueContainer;
    
    [Header("Text Asset")]
    [SerializeField] private TextAsset _inkJSON;

    public TextAsset InkJSON => GetInkJSON();
    // public DialogueContainer DialogueContainer => _dialogueContainer;
    
    
    
    protected virtual void Start()
    {
        _onDialogueEnd.OnRaised += OnDialogueEnd;
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
        Debug.Log("Dialogue Ended Base Class");
    }
    
    private TextAsset GetInkJSON()
    {
        return _inkJSON;
    }
    
}
