using System.Collections;
using System.Collections.Generic;
using com.LazyGames.DZ;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.Dz.Ai
{
    public enum EnemyState { Idle, Patrolling, Investigating, Chasing, Attacking, Stunned }

    [SelectionBase]
    public class EnemyBt : Tree, INoiseSensitive, IGadgetInteractable
    {
        public EnemyState State => _state;

        public EnemyParameters parameters;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private EnemyWayPoints enemyWayPoints;
        [SerializeField] private float stunTime = 5f;
        [SerializeField] private TypeOfGadget stunElement;

        private Node _root;
        private EnemyState _state;
        private NavMeshAgent _agent;
        private EnemyAnimHandler _animHandler;

        protected override Node SetupTree()
        {
            Prepare();
            _root = BuildTree();
            _state = EnemyState.Patrolling;
            return _root;
        }

        private void Update()
        {
            _root?.Evaluate(_state == EnemyState.Stunned);
        }

        private Node BuildTree()
        {
            var t = transform;
            _root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckCanSeeTarget(t, parameters),
                    new CheckPlayerInAttackRange(t, parameters),
                    new TaskGoToTarget(t, parameters),
                    new TaskLookAt(t),
                    new TaskAttack(t, parameters),
                }),
                new Sequence(new List<Node>
                {
                    new CheckPlayerInFov(t, parameters, playerLayer),
                    new TaskGoToTarget(t, parameters),
                }),
                new TaskSearchLastKnownPosition(t, parameters),
                new TaskInvestigateNoise(t),
                new TaskPatrol(t, enemyWayPoints.WayPoints.ToArray(), parameters),
            });

            return _root;
        }

        private void Prepare()
        {
            _agent = GetComponent<NavMeshAgent>();
            var animator = GetComponentInChildren<Animator>();
            var animHandler = GetComponent<EnemyAnimHandler>();
            animHandler.Initialize(animator, this);
        }

        private Vector3 DirFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public void HearNoise(float intensity, Vector3 position, bool dangerous)
        {
            Debug.Log("heard noise");
            object t = _root.GetData("target");
            if (t != null) return;
            _root.WipeData();
            _root.SetData("NoisePosition", position);
        }

        public void GadgetInteraction(TypeOfGadget interactedGadget)
        {
            if (interactedGadget != stunElement) return;
            Stun();
        }

        private void Stun()
        {
            _state = EnemyState.Stunned;
            _agent.isStopped = true;
            StartCoroutine(CorStunTime());
        }

        private IEnumerator CorStunTime()
        {
            yield return new WaitForSeconds(stunTime);
            _state = EnemyState.Idle;
            _agent.isStopped = false;
        }
    }
}
