using System.Collections;
using DINO.Utility;
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
        base.ContinueDialogue();
        SetTimer();
    }

    protected override void OnDialogueEnd()
    {
        base.OnDialogueEnd();
        CleanTimer();
    }

    private void CleanTimer()
    {
        _timerBase.OnTimerEnd -= OnFinishedDialogueTimer;
        _timerBase.StopTimer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Interact with trigguer Dialogue");
            SendDialogue();
        }
    }

    private void SetTimer()
    {
        if(_timerBase == null)
            _timerBase = gameObject.AddComponent<TimerBase>();
            
        if(_timerBase.IsTimerActive) return;
            
        _timerBase.OnTimerEnd += OnFinishedDialogueTimer;
        _timerBase.StartTimer(_timeToContinue);
    }


    private void OnFinishedDialogueTimer()
    {
        ContinueDialogue();
        CleanTimer();
        
    }
    
    
}
