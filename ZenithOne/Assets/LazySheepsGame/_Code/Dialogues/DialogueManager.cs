using System.Collections.Generic;
using Ink.Runtime;
using Obvious.Soap;
using UnityEngine;

namespace com.LazyGames
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] ScriptableEventDialogueBase _onDialogueSend;
        [SerializeField] ScriptableEventNoParam _onContinueDialogue;
        [SerializeField] ScriptableEventNoParam _onDialogueEnd;

        private Story _currentStory;
        private DialogueBase _currentDialogue;
        private string _currentSpeaker;
        private string _currentVoice;
        private int _currentDialogueIndex;
        
        private int CountDialogueLines(string inkText)
        {
            var lines = inkText.Split('\n');
            int dialogueLinesCount = 0;

            foreach (var line in lines)
            {
                // Considera que una línea de diálogo es cualquier línea que no esté vacía y no comienza con un comentario
                if (!string.IsNullOrWhiteSpace(line) && !line.TrimStart().StartsWith("//"))
                {
                    dialogueLinesCount++;
                }
            }

            return dialogueLinesCount;
        }
        
        

        private const string SPEAKER_TAG = "speaker";
        private const string VOICE_TAG = "voice";


        private void Start()
        {
            _onDialogueSend.OnRaised += EnterDialogueMode;
            _onContinueDialogue.OnRaised += ContinueStory;
        }

        private void EnterDialogueMode(DialogueBase dialogue)
        {
            _onDialogueSend.OnRaised -= EnterDialogueMode;
            _currentDialogue = dialogue;
            TextAsset inkJSON = dialogue.InkJSON;
            _currentStory = new Story(inkJSON.text);

            ContinueStory();

        }

        private void ContinueStory()
        {
            if(_currentStory == null)
            {
                // Debug.LogError("Current story is null");
                return;
            }
            
            if (_currentStory.canContinue)
            {
                _currentStory.Continue();
                SetTags(_currentStory.currentTags);
                _currentDialogueIndex++;
                
                DialogueInfoUI dialogueInfoUI = new DialogueInfoUI();
                dialogueInfoUI.Text = _currentStory.currentText;
                dialogueInfoUI.Speaker = _currentSpeaker;
                dialogueInfoUI.Voice = _currentVoice;
                dialogueInfoUI.CurrentDialogueIndex = _currentDialogueIndex;
                // dialogueInfoUI.TotalDialogueLines = CountDialogueLines(_currentStory.);
                
                Debug.Log("Current Dialogue Index: ".SetColor("#41E85D") + _currentDialogueIndex);
                Debug.Log("Total Dialogue Lines: ".SetColor("#41E85D") + dialogueInfoUI.TotalDialogueLines);
                
                SendInfoToUI(dialogueInfoUI);
            }
            else
            {
                ExitDialogueMode();
            }
        }
        
        private void ExitDialogueMode()
        {
            _onDialogueSend.OnRaised += EnterDialogueMode;
            _onDialogueEnd.Raise();
            
            _currentDialogue = null;
            _currentStory = null;
            _currentSpeaker = null;
            _currentVoice = null;
            _currentDialogueIndex = 0; 
            
            Debug.Log("Dialogue ended");   
            
        }

        private void SetTags(List<string> currentTags)
        {
            foreach (var tag in currentTags)
            {
                string[] tagParts = tag.Split(':');
                
                if(tagParts.Length != 2) 
                    Debug.LogError("Invalid tag format: " + tag);
                
                string tagKey = tagParts[0].Trim();
                string tagValue = tagParts[1].Trim();
                
                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        Debug.Log("speaker: " + tagValue);
                        _currentSpeaker = tagValue;
                        
                        break;
                    case VOICE_TAG:
                        Debug.Log("voice: " + tagValue);
                        _currentVoice = tagValue;
                        break;
                    default:
                        Debug.LogWarning("Unknown tag:" + tagKey);  
                        break;
                }
            }
        }
        
        private void SendInfoToUI(DialogueInfoUI dialogueInfoUI)
        {
            if(_currentDialogue is DialogueNPC npc)
            {
                npc.SetDialogueToNpc(dialogueInfoUI);
            }
            else
            {
                Debug.LogError("Current dialogue is not a NPC");
            }
        }
        
        
        
        
    }
    
    public struct DialogueInfoUI
    {
        public string Speaker;
        public string Voice;
        public string Text;
        public int CurrentDialogueIndex;
        public int TotalDialogueLines;
        
        
    }

}

