using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using UnityEngine;

public class DialogueBase : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private ScriptableEventTextAsset _onDialogueSend;
    
    [Header("Container")]
    [SerializeField] private DialogueContainer _dialogueContainer;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _onDialogueSend.Raise(_dialogueContainer.InkJSON);
            Debug.Log("Dialogue Sent".SetColor("#3CE4FF"));
        }
    }
}
