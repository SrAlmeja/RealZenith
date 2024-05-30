using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace com.LazyGames.Dz.Ai
{
    public class BotBt : Tree
    {
        private Node _root;
        private NavMeshAgent _agent;
        private Animator _animator;
        [SerializeField]private EnemyParameters parameters;
        
        protected override Node SetupTree()
        {
           Prepare();
           _root = BuildTree();
           return _root;
        }

        void Update()
        {
            
        }
        private Node BuildTree()
        {
            var t = transform;
            _root = new Selector(new List<Node>
            {
                    new TaskDispense(t),
                new Sequence(new List<Node>
                {
                    new CheckPlayerInRange(t, parameters),
                    new TaskFaceTarget(t, parameters),
                }),
                new TaskWander(t)
            });
            return _root;
        }

        
        private void Prepare()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            // parameters = 
        }
    }
}
