using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Obvious.Soap;
using TMPro;
using UnityEngine;

public class DialogueNPC : DialogueBase
{
    [Header("UI")]
    [SerializeField] private GameObject _dialogueMeshUI;
    [SerializeField] private TextMeshProUGUI _dialogueText;


    private void Start()
    {
        _dialogueMeshUI.SetActive(false);
    }
    
    public void SetDialogueText(string text)
    {
        _dialogueMeshUI.SetActive(true);
        
        DisplayTextEffect(text);
    }

    private void DisplayTextEffect(string text)
    {
        DOTweenTMPAnimator doTweenTMPAnimator = new DOTweenTMPAnimator(_dialogueText);
        _dialogueText.text = "";
        _dialogueText.DOText(text, text.Length * 0.05f).SetEase(Ease.Linear).OnComplete(() =>
        { 
            // _dialogueMeshUI.SetActive(false);
        });

    }
    
    
    
}
