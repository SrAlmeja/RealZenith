using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue Container")]
public class DialogueContainer : ScriptableObject
{
    
    public TextAsset InkJSON;
    public AudiosDialogueData[] AudiosDialogueData;
    
    
    
}

[Serializable]
public class AudiosDialogueData
{
    public string Section;
    public AudioClip[] DialoguesAudioClips;
}