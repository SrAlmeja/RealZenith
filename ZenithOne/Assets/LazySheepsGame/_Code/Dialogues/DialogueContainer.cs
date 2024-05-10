using System;
using System.Collections.Generic;
using Ink.Runtime;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue Container")]
public class DialogueContainer : ScriptableObject
{
    public List<DialogueSection> DialogueSections;
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
    public string id_Ink;
    public TextAsset InkJSON;
    public bool IsDialogueEnd;
    public DialogueType DialogueType;
    
    
}

[Serializable]
public class DialogueSection
{
    public string Section;
    public List<InksContainers> InksContainers;
    public bool IsSectionEnd;
}

public enum DialogueType
{
    Subtitles,
    NPC,
}