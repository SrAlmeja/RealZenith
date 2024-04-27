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

        private Story _currentStory;
        private DialogueBase _currentDialogue;

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
                Debug.LogError("Current story is null");
                return;
            }
            
            if (_currentStory.canContinue)
            {
                Debug.Log(_currentStory.Continue());
                SendInfoToUI(_currentStory.currentText);
                HandleTags(_currentStory.currentTags);
            }
            else
            {
                ExitDialogueMode();
            }
        }
        
        private void ExitDialogueMode()
        {
            _onDialogueSend.OnRaised += EnterDialogueMode;
            _currentDialogue = null;
            _currentStory = null;
            
            Debug.Log("Dialogue ended");   
            
        }

        private void HandleTags(List<string> currentTags)
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
                        Debug.Log("speaker:" + tagValue);
                        
                        break;
                    case VOICE_TAG:
                        Debug.Log("voice:" + tagValue);
                        
                        break;
                    default:
                        Debug.LogWarning("Unknown tag:" + tagKey);  
                        break;
                }
            }
        }
        
        private void SendInfoToUI(string text)
        {
            if(_currentDialogue is DialogueNPC npc)
            {
                npc.SetDialogueText(text);
            }
            else
            {
                Debug.LogError("Current dialogue is not a NPC");
            }
        }
        
        
        
        
    }

}