using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    [SelectionBase]
    public class BotBt : Tree
    {

        [Header("Bot Parameters")] 
        [SerializeField] private GameObject prefabToDispense;
        [SerializeField]private BotParameters parameters;
        
        [Header("Serialized References")]
        [SerializeField] private Transform dispenser1;
        [SerializeField] private Transform dispenser2;
        [SerializeField] private GameObject door1;
        [SerializeField] private GameObject door2;
        private Node _root;
        
        protected override Node SetupTree()
        {
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
                new TaskDispense(t, parameters, prefabToDispense),
                new TaskFaceTarget(t, parameters),
                new TaskWander(t, parameters)
            });
            return _root;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _root.SetData("target", other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _root.ClearData("target");
        }

    }
}
