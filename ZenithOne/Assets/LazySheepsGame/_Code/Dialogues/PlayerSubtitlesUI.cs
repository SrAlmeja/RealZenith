using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using TMPro;
using UnityEngine;

public class PlayerSubtitlesUI : MonoBehaviour
{
    public static PlayerSubtitlesUI Instance;
    
    
    [SerializeField] private GameObject _subtitlesUI;
    [SerializeField] private TextMeshProUGUI _subtitlesText;
    
    string _currentText;
    
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }



    public void SetUISubtitles(DialogueInfoUI dialogueInfoUI)
    {
        // DisplayText(dialogueInfoUI.Text);
    }

    public void DisplayText(string text)
    {
        _currentText = text;
        _subtitlesText.text = _currentText;

        StartCoroutine(HideSubtitles());
    }
    
    private IEnumerator HideSubtitles()
    {
        yield return new WaitForSeconds(5);
        _subtitlesText.text = "";
    }
    
    
}
