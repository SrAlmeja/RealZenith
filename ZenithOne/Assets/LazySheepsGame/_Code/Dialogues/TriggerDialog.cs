using System.Collections;
using com.LazyGames;
using DINO.Utility;
using Ink.Parsed;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerDialog : DialogueBase
{
    [SerializeField] private float _timeToContinue = 4f;
    private TimerBase _timerBase;
    
    
    public override void SendDialogue()
    {
        base.SendDialogue();
        SetTimer();

    }

    public override void ContinueDialogue()
    {
        SetTimer();
        base.ContinueDialogue();
    }

    protected override void OnDialogueEnd()
    {
        base.OnDialogueEnd();
        CleanTimer();
    }

    private void CleanTimer()
    {
        if (_timerBase == null)
        {
            Debug.LogError("Timer is null FROM" + gameObject.name);
            return;
        }

        Debug.Log("Clean Timer");
        _timerBase.OnTimerEnd -= OnFinishedDialogueTimer;
        _timerBase.StopTimer();
        Destroy(_timerBase);
        _timerBase = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Interact with trigguer Dialogue");
            SendDialogue();
        }
    }

    private void SetTimer()
    {
        if(_timerBase == null)
            _timerBase = gameObject.AddComponent<TimerBase>();
            
        // if(_timerBase.IsTimerActive) return;
            
        _timerBase.OnTimerEnd += OnFinishedDialogueTimer;
        _timerBase.StartTimer(_timeToContinue);
        Debug.Log("Set Timer");
    }


    private void OnFinishedDialogueTimer()
    {
        CleanTimer();
        ContinueDialogue();
        
        
    }
    
    
}
