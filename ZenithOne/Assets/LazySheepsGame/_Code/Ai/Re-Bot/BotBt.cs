using System;
using System.Collections.Generic;
using FMODUnity;
using Obvious.Soap;
using TMPro;
using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    [SelectionBase]
    public class BotBt : Tree
    {

        [Header("Bot Parameters")] 
        [SerializeField] private GameObject prefabToDispense;
        [SerializeField] private BotParameters parameters;
        [SerializeField] private int gadgetLimit = 2;
        
        [Header("Serialized References")]
        [SerializeField] private Transform dispenser1;
        [SerializeField] private Transform dispenser2;
        [SerializeField] private StudioEventEmitter emitter;
        [SerializeField] private ScriptableVariable<int> gadgetInventory;
        [SerializeField] private TextMeshPro displayText;
        private Node _root;
        private BotDoorController _doorController;
        
        protected override Node SetupTree()
        {
            _doorController = GetComponent<BotDoorController>();
            displayText.text = (gadgetLimit - gadgetInventory.Value ).ToString();
           _root = BuildTree();
           return _root;
        }

        void Update()
        {
            _root.Evaluate(false);
        }
        private Node BuildTree()
        {
            var t = transform;
            _root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new TaskFaceTarget(t, parameters),
                    new TaskDispense(t, parameters, prefabToDispense, dispenser1, dispenser2, emitter, gadgetLimit, gadgetInventory, displayText)
                }),
                new TaskWander(t, parameters)
            });
            return _root;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _root.SetData("target", other.transform);
            Debug.Log(gameObject.name + "got target");
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _root.ClearData("target");
            _doorController.CloseDoor();
            Debug.Log(gameObject.name + "lost target");
        }
    }
}
