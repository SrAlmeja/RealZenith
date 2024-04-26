using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using UnityEngine;

public class DialogueBase : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private ScriptableEventDialogueBase _onDialogueSend;
    
    [Header("Container")]
    [SerializeField] private DialogueContainer _dialogueContainer;
    public TextAsset InkJSON => _dialogueContainer.InkJSON;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _onDialogueSend.Raise(this);
        }
    }
}
