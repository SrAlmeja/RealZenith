using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using TMPro;
using UnityEngine;

public class PlayerSubtitles : MonoBehaviour
{
    public static PlayerSubtitles Instance;
    
    [SerializeField] ScriptableEventDialogueBase _onDialogueSend;
    [SerializeField] ScriptableEventNoParam _onDialogueEnd;
    [SerializeField] private GameObject _subtitlesUI;
    [SerializeField] private TextMeshProUGUI _subtitlesText;
    
    string _currentText;
    
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _onDialogueSend.OnRaised += OnStartDialogue;
        _onDialogueEnd.OnRaised += OnEndDialogue;
        
    }

    private void OnEndDialogue()
    {
        if (_subtitlesUI.activeSelf && !String.IsNullOrEmpty(_currentText))
        {
            _subtitlesUI.SetActive(false);
            _currentText = "";
        }
    }

    private void OnStartDialogue(DialogueBase dialogue)
    {
        // if (dialogue.DialogueContainer.DialogueType == DialogueType.Subtitles)
        // {
        //     _subtitlesUI.SetActive(true);
        // }
    }

    public void SetUISubtitles(DialogueInfoUI dialogueInfoUI)
    {
        DisplayText(dialogueInfoUI.Text);
    }

    private void DisplayText(string text)
    {
        _currentText = text;
        _subtitlesText.text = _currentText;
    }
    
    
}
