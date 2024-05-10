using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue Container")]
public class DialogueContainer : ScriptableObject
{
    public List<InksContainers> InksContainers;
    public AudiosDialogueData[] AudiosDialogueData;
    public bool IsDialogueEnd;
    public DialogueType DialogueType;
   
    
    
}

[Serializable]
public class AudiosDialogueData
{
    public string Section;
    public AudioDialogue[] AudioDialogues;
}
[Serializable]
public class AudioDialogue
{
    public string ID;
    public AudioClip DialogueAudioClip;
}

[Serializable]
public class InksContainers
{
    public string Section;
    public TextAsset InkJSON;
}

public enum DialogueType
{
    Subtitles,
    NPC,
}