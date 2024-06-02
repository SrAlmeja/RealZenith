using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using com.LazyGames;
using Obvious.Soap;
using TMPro;
using UnityEngine;

public class PlayerSubtitlesUI : MonoBehaviour
{
    public static PlayerSubtitlesUI Instance;
    [SerializeField] private GameObject _subtitlesUI;
    [SerializeField] private TextMeshProUGUI _subtitlesText;
    [SerializeField] private Hand _hand;
    
    string _currentText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerHaptic();
        }
        
    }

    private void Awake()
    {
        Instance = this;
        
    }

    public void TriggerHaptic()
    {
        float handAmp = 1f; 
        // _hand.PlayHapticVibration(0.5f);
        Debug.Log("Haptic");
    }
    
    
    public void DisplayText(string text)
    {
        TriggerHaptic();
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
