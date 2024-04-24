using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Obvious.Soap;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] ScriptableEventTextAsset _onDialogueSend;
    
    private Story _currentStory;


    private void Start()
    {
        _onDialogueSend.OnRaised += EnterDialogueMode;
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        _currentStory = new Story(inkJSON.text);
        
        Debug.Log(_currentStory.currentText);

        if (_currentStory.canContinue)
        {
            Debug.Log(_currentStory.Continue());
        }else
        {
            Debug.Log("No more content");
        }
        
    }

}
