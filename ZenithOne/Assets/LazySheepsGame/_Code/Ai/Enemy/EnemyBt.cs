using System.Collections;
using System.Collections.Generic;
using CryoStorage;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.Dz.Ai
{
    // public enum EnemyState { Waiting, Moving, Investigating, Chasing, Attacking, Stunned, None }

    [SelectionBase]
    public class EnemyBt : Tree, IGadgetInteractable
    {

        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private EnemyWayPoints enemyWayPoints;
        [SerializeField] private float stunTime = 5f;
        [SerializeField] private TypeOfGadget stunElement;
        [Header("Parameters")]
        [SerializeField] private EnemyParameters defaultParameters;
        [SerializeField] private EnemyParameters alertParameters;
        
        private Node _root;
        // private EnemyState _state;
        private NavMeshAgent _agent;
        // private EnemyAnimHandler _animHandler;
        private Animator _animator;

        private EnemyVision _vision;
        private EnemyHearing _hearing;
        private EnemyParameters _parameters;
        private bool _stunned;
        private bool _startled;
        
        private static readonly int Stunned = Animator.StringToHash("stunned");
        private static readonly int Moving = Animator.StringToHash("moving");
        private static readonly int Chasing = Animator.StringToHash("chasing");

        protected override Node SetupTree()
        {
            Prepare();
            _root = BuildTree();
            return _root;
        }

        private void Update()
        {
            _root?.Evaluate(_stunned);
            _vision.Step();
            _agent.speed = _parameters.movementSpeed;
            _animator.SetBool(Moving, _agent.velocity.magnitude > 0.3f);
        }

        private Node BuildTree()
        {
            var t = transform;
            _root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckHasTarget(t, _parameters),
                    new CheckPlayerInAttackRange(t, _parameters),
                    new TaskGoToTarget(t, _parameters),
                    // new TaskLookAt(t),
                    new TaskAttack(t, _parameters),
                }),
                new Sequence(new List<Node>
                {
                    new CheckHasTarget(t, _parameters),
                    new TaskGoToTarget(t, _parameters),
                }),
                new TaskSearchLastKnownPosition(t, _parameters),
                new TaskInvestigateNoise(t,_parameters),
                new TaskPatrol(t, enemyWayPoints.WayPoints.ToArray(), _parameters),
            });

            return _root;
        }
        
        public void GadgetInteraction(TypeOfGadget interactedGadget)
        {
            if (interactedGadget != stunElement) return;
            Stun();
        }


        public void PlayerDetected(Transform target)
        {
            _root.SetData("target", target);
            _parameters = alertParameters;
            _agent.speed = _parameters.movementSpeed;
            _vision.Parameters.coneAngle = _parameters.coneAngle;
            _animator.SetBool(Chasing, true);

            StopAllCoroutines();
            
            if(_startled) return;
            _animator.Play("enemy_startled");
            _startled = true;
        }
        
        public void PlayerLost(Vector3 lastKnownPosition)
        {
            _root.WipeData();
            _root.SetData("lastKnownPosition", lastKnownPosition);
            StartCoroutine(CorAlertCooldown());
        }
        
        private IEnumerator CorAlertCooldown()
        {
            yield return new WaitForSeconds(_parameters.alertTime);
            _parameters = defaultParameters;
            _vision.Parameters.coneAngle = _parameters.coneAngle;
            _vision.Parameters.coneAngle = _parameters.coneAngle;
            _animator.SetBool(Chasing, true);
            _startled = false;

        }
        
        public void NoiseHeard(Vector3 noisePosition)
        {
            // Debug.Log("heard noise");
            object t = _root.GetData("target");
            if (t != null) return;
            _root.WipeData();
            _root.SetData("NoisePosition", noisePosition);
            if(_startled) return;
            _animator.Play("enemy_startled");
            _startled = true;
        }
        
        private void Stun()
        {
            _stunned = true;
            _agent.isStopped = true;
            _animator.Play("enemy_stunned");
            _animator.SetBool(Stunned, true);
            // _animator.SetBool(Stunned, true);
            // _animator.Play("enemy_stunned");
            StartCoroutine(CorStunTime());
        }
        private IEnumerator CorStunTime()
        {
            yield return new WaitForSeconds(_parameters.stunTime);
            _stunned = false;
            _agent.isStopped = false;
            _animator.SetBool(Stunned, false);
        }
        
        private void Prepare()
        {
            _parameters = ScriptableObject.CreateInstance<EnemyParameters>();
            _parameters = defaultParameters;
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _parameters.movementSpeed;
            _hearing = gameObject.AddComponent<EnemyHearing>();
            _vision = gameObject.AddComponent<EnemyVision>();
            _animator = GetComponentInChildren<Animator>();
            
            // var animHandler = GetComponent<EnemyAnimHandler>();
            // animHandler.Initialize(_animator, this);
            
            _hearing.Initialize(this);
            _vision.Initialize(this, _parameters, LayerMask.GetMask("Player"));
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(_parameters == null) return;
            var position = transform.position + _parameters.heightOffset;
            Handles.color = Color.white;
            Handles.DrawWireDisc(position, Vector3.up, _parameters.detectionRange);
        
            var eulerAngles = transform.eulerAngles;
            Vector3 viewAngle01 = CryoMath.DirFromAngle(eulerAngles.y, -_parameters.coneAngle / 2);
            Vector3 viewAngle02 = CryoMath.DirFromAngle(eulerAngles.y, _parameters.coneAngle / 2);
        
            Handles.color = Color.yellow;
            Handles.DrawLine(position, position + viewAngle01 * _parameters.detectionRange);
            Handles.DrawLine(position, position + viewAngle02 * _parameters.detectionRange);
        }
        #endif

    }
}
