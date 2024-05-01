using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using DG.Tweening;
using Obvious.Soap;
using TMPro;
using UnityEngine;

public class DialogueNPC : DialogueBase
{
    [Header("UI")]
    [SerializeField] private GameObject _dialogueMeshUI;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _speakerText;
    [SerializeField] private TextMeshProUGUI _numberText;

    private string _currentText;
    private bool _isDialogueActive;

    private void Start()
    {
        _dialogueMeshUI.SetActive(false);
    }
    
    public void SetDialogueToNpc(DialogueInfoUI dialogueInfoUI)
    {
        _dialogueMeshUI.SetActive(true);
        DisplayTextEffect(dialogueInfoUI.Text);
        SetSpeaker(dialogueInfoUI.Speaker);
    }

    protected override void OnDialogueEnd()
    {
        base.OnDialogueEnd();
        _currentText = "";
        _dialogueText.text = "";
        _dialogueMeshUI.SetActive(false);
    }

    private void SetSpeaker(string speaker)
    {
        // Debug.Log("Speaker: ".SetColor("#41E8B8") + speaker);
        _speakerText.text = speaker;
    }
    private void DisplayTextEffect(string text)
    {
        if (_isDialogueActive)
        {
            // SkipTextEffect(_currentText);
            return;
        }
        
        _currentText = text;
        _dialogueText.text = "";
        _dialogueText.DOText(_currentText, _currentText.Length * 0.03f).SetEase(Ease.Linear).OnComplete(() =>
        {
            _isDialogueActive = false;
        });
        
        
    }
    
    private void SkipTextEffect(string text)
    {
        _dialogueText.DOKill();
        _dialogueText.text = text;
        _isDialogueActive = false;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SendDialogue();
        }
    }
}
