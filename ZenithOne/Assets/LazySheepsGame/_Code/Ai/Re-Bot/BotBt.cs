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
        private Node _root;
        private BotDoorController _doorController;
        
        protected override Node SetupTree()
        {
            _doorController = GetComponent<BotDoorController>();
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
                    new TaskDispense(t, parameters, prefabToDispense, dispenser1, dispenser2)
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
