using System;
using System.Collections.Generic;
using Ink.Runtime;
using Obvious.Soap;
using UnityEngine;

namespace com.LazyGames
{
    public class DialogueManager : MonoBehaviour
    {
        private static DialogueManager _instance;
        public static DialogueManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<DialogueManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject();
                        go.name = "DialogueManager";
                        _instance = go.AddComponent<DialogueManager>();
                        DontDestroyOnLoad(_instance);
                    }
                }
                

                return _instance;
            }
        }
        
        [SerializeField] ScriptableEventDialogueBase _onDialogueSend;
        [SerializeField] ScriptableEventNoParam _onContinueDialogue;
        [SerializeField] ScriptableEventNoParam _onDialogueEnd;

        [SerializeField] private List<DialogueBase> _dialoguesInScene;

        public event Action<string> OnFinishedDialogue;
        public List<DialogueBase> DialoguesInScene => _dialoguesInScene;
        private Story _currentStory;
        private DialogueBase _currentDialogueBase;
        private string _currentSpeaker;
        private string _currentVoice;
        
        public Story CurrentStory => _currentStory;
        

        private const string SPEAKER_TAG = "speaker";
        private const string VOICE_TAG = "voice";

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _onDialogueSend.OnRaised += EnterDialogueMode;
            _onContinueDialogue.OnRaised += ContinueStory;
        }

        private void EnterDialogueMode(DialogueBase dialogue)
        {
            _onDialogueSend.OnRaised -= EnterDialogueMode;
            _currentDialogueBase = dialogue;
            TextAsset inkJSON = dialogue.GetInkJSON();
            
            
            if(inkJSON == null)
            {
                Debug.LogError("No Dialogue to trigger");
                return;
            }
            
            _currentStory = new Story(inkJSON.text);
            // Debug.Log("cURRENT STORY: ".SetColor("") + _currentStory.);

            
            if (_currentDialogueBase.HasDialogueToTrigger)
            {
                _currentDialogueBase.CurrentInkContainer.OnDialogueEnd += TriggerDialogueSubtitle;
            }
            
            ContinueStory();

        }

        private void ContinueStory()
        {
            if(_currentStory == null)
            {
                Debug.LogError("Current story is null");
                ExitDialogueMode();
                return;
            }
            
            DialogueInfoUI dialogueInfoUI = new DialogueInfoUI();
            dialogueInfoUI.CanContinue = _currentStory.canContinue;
            
            if (_currentStory.canContinue)
            {
                _currentStory.Continue();
                SetTags(_currentStory.currentTags);
                dialogueInfoUI.Text = _currentStory.currentText;
                dialogueInfoUI.Speaker = _currentSpeaker;
                dialogueInfoUI.Voice = _currentVoice;
                
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
            
            if (_currentDialogueBase.HasDialogueToTrigger)
            {
                Debug.Log("Remove Trigger");
                _currentDialogueBase.CurrentInkContainer.OnDialogueEnd -= TriggerDialogueSubtitle;
            }
            
            OnFinishedDialogue?.Invoke(_currentDialogueBase.CurrentInkContainer.id_Ink);

            _currentDialogueBase = null;
            _currentStory = null;
            _currentSpeaker = null;
            _currentVoice = null;
            
            
            
            // Debug.Log("Dialogue ended");   
            
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
                        // Debug.Log("speaker: " + tagValue);
                        _currentSpeaker = tagValue;
                        
                        break;
                    case VOICE_TAG:
                        // Debug.Log("voice: " + tagValue);
                        _currentVoice = tagValue;
                        break;
                    default:
                        // Debug.LogWarning("Unknown tag:" + tagKey);  
                        break;
                }
            }
        }
        
        private void SendInfoToUI(DialogueInfoUI dialogueInfoUI)
        {
            if(_currentDialogueBase is DialogueNPC npc)
            {
                npc.SetDialogueToNpc(dialogueInfoUI);
            }
            else if (_currentDialogueBase is MarthDialog triggerDialog)
            {
                if(triggerDialog.CurrentInkContainer.DialogueType == DialogueType.Subtitles)
                {
                    PlayerSubtitlesUI.Instance.SetUISubtitles(dialogueInfoUI);
                }
            }
        }

        private void TriggerDialogueSubtitle(string dialogueToTrigger, CHARACTER character)
        {
            Debug.Log("Trigger Dialogue: ".SetColor("#C5F335") + dialogueToTrigger);
            DialogueBase dialogueBase = _dialoguesInScene.Find(x => x.Character == character);
            
            if(dialogueBase == null)
            {
                Debug.LogError("Dialogue Base is null");
                return;
            }
            
            Debug.Log("Dialogue Base: ".SetColor("#C5F335") + dialogueBase.name);
            dialogueBase.SendDialogue();
        }
        
        public void FindDialogueInScene()
        {
            _dialoguesInScene = new List<DialogueBase>();
            DialogueBase[] dialogues = FindObjectsOfType<DialogueBase>();
            foreach (var dialogue in dialogues)
            {
                _dialoguesInScene.Add(dialogue);
            }
        }
        
        
    }
    
    public struct DialogueInfoUI
    {
        public string Speaker;
        public string Voice;
        public string Text;
        public bool CanContinue;
        
        
    }

}
