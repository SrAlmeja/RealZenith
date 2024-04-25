using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using TMPro;
using UnityEngine;

public class DialogueNPC : DialogueBase
{
    [Header("UI")]
    [SerializeField] private GameObject _dialogueMeshUI;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    
    
    public void SetDialogueText(string text)
    {
        _dialogueMeshUI.SetActive(true);
        _dialogueText.text = text;
    }
    
    
    
    
}
