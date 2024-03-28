using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue Container")]
public class DialogueContainer : ScriptableObject
{    
    public string name;
    public Dialogue[] dialogues;
    
}

[Serializable]
public class Dialogue
{
    public bool _canRepeat;
    public bool _hasFinished;
    public string[] sentences;
    
    public bool CanRepeat
    {
        get => _canRepeat;
        set => _canRepeat = value;
    }
    
    public bool HasFinished
    {
        get => _hasFinished;
        set => _hasFinished = value;
    }
}