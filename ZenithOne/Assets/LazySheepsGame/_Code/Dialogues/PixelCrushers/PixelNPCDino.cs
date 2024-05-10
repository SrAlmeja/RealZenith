using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class PixelNPCDino : MonoBehaviour, IDialogueUI
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public event EventHandler<SelectedResponseEventArgs> SelectedResponseHandler;
    public void Open()
    {
    }

    public void Close()
    {
    }

    public void ShowSubtitle(Subtitle subtitle)
    {
    }

    public void HideSubtitle(Subtitle subtitle)
    {
    }

    public void ShowResponses(Subtitle subtitle, Response[] responses, float timeout)
    {
    }

    public void HideResponses()
    {
    }

    public void ShowQTEIndicator(int index)
    {
    }

    public void HideQTEIndicator(int index)
    {
    }

    public void ShowAlert(string message, float duration)
    {
    }

    public void HideAlert()
    {
    }
}
