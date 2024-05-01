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
    [SerializeField] private DialogueContainer _dialogueContainer;
    public TextAsset InkJSON => _dialogueContainer.InkJSON;
    public 
    void Start()
    {
        _onDialogueEnd.OnRaised += OnDialogueEnd;
    }
    
    public void SendDialogue()
    {
        _onDialogueSend.Raise(this);
    }
    
    public void ContinueDialogue()
    {
        _onConinueDialogue.Raise();
    }

     protected virtual void OnDialogueEnd()
    {
        Debug.Log("Dialogue Ended Base Class");
    }
    
    
}
