// Modificado Raymundo Mosqueda 09/05/24

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace com.LazyGames.Dz.Ai
{
    [SelectionBase]
    public class EnemyBt : Tree, IGadgetInteractable
    {
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private EnemyWayPoints enemyWayPoints;
        [SerializeField] private GameObject vfxObject;
        [Header("Parameters")]
        [SerializeField] private EnemyParameters defaultParameters;
        [SerializeField] private EnemyParameters alertParameters;
        
        private Node _root;
        private NavMeshAgent _agent;
        private Animator _animator;
        private EnemyVision _vision;
        private EnemyHearing _hearing;
        private LayerSwitcher _layerSwitcher;
        private EnemyParameters _parameters;
        private bool _isStunned;
        private bool _startled;
        
        private static readonly int Stunned = Animator.StringToHash("stunned");
        private static readonly int Moving = Animator.StringToHash("moving");
        private static readonly int Chasing = Animator.StringToHash("chasing");
        private static readonly int Attacking = Animator.StringToHash("attacking");
        private static readonly int Alert = Animator.StringToHash("alert");


        protected override Node SetupTree()
        {
            Prepare();
            _root = BuildTree();
            return _root;
        }

        private void Update()
        {
            _root?.Evaluate(_isStunned);
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
                    new CheckHasTarget(_parameters),
                    new CheckPlayerInAttackRange(t, _parameters),
                    new TaskGoToTarget(t, _parameters),
                    new Sequence(new List<Node>
                    {
                        new TaskAttack(t, _parameters),
                        new TaskFaceTarget(t, _parameters), 
                    }),
                }),
                new Sequence(new List<Node>
                {
                    new CheckHasTarget(_parameters),
                    new TaskGoToTarget(t, _parameters),
                }),
                new TaskSearchLastKnownPosition(t, _parameters),
                new TaskInvestigateNoise(t,_parameters),
                new TaskPatrol(t, enemyWayPoints.WayPoints.ToArray()),
            });

            return _root;
        }
        
        public void GadgetInteraction(TypeOfGadget interactedGadget)
        {
            switch (interactedGadget)
            {
                case TypeOfGadget.WaterGranade:
                    Debug.Log("water grenade interaction"); 
                    Stun();
                    break;
                case TypeOfGadget.RadarGranade:
                    Debug.Log("radar grenade interaction"); 
                    StopCoroutine(nameof(CorDisableHighlight));
                    Debug.Log("enabling highlight");
                    foreach (var child in vfxObject.transform)
                    {
                        var goChild = (Transform) child;
                        _layerSwitcher.OnSelected(goChild.gameObject);
                    }
                    StartCoroutine(CorDisableHighlight());
                    break;
            }
        }

        private IEnumerator CorDisableHighlight()
        {
            yield return new WaitForSeconds(_parameters.revealTime);
            Debug.Log("disabling highlight");
            foreach (var child in vfxObject.transform)
            {
                var goChild = (Transform) child;
                _layerSwitcher.OnDeselected(goChild.gameObject);
            }
        }

        public void ResetPosition()
        {
            _root.WipeData();
            _vision.ResetVision();
            var rand = Random.Range(0, enemyWayPoints.WayPoints.Count);
            _agent.Warp(enemyWayPoints.WayPoints[rand].transform.position);
            
            Invoke(nameof(DelayedWipe), .1f);
        }   

        public void PlayerDetected(Transform target)
        {
            _root.SetData("target", target);
            _parameters = alertParameters;
            _agent.speed = _parameters.movementSpeed;
            _vision.Parameters.coneAngle = _parameters.coneAngle;
            _animator.SetBool(Chasing, true);

            StopCoroutine(nameof(CorAlertCooldown));
            
            if(_startled) return;
            _animator.CrossFade("enemy_startled",0.2f);
            _startled = true;
        }

        // avoids race condition where player is lost after resetting
        private void DelayedWipe()
        {
            _root.WipeData();
            _animator.SetBool(Attacking, false);
            _animator.SetBool(Chasing, false);
            _animator.SetBool(Stunned, false);
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
            _animator.SetBool(Chasing, false);
            _animator.SetBool(Alert, false);
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
            _animator.CrossFade("enemy_startled",0.2f);
            _startled = true;
        }
        
        private void Stun()
        {
            _isStunned = true;
            _agent.isStopped = true;
            _animator.Play("enemy_enter_stun");
            _animator.SetBool(Stunned, true);
            StartCoroutine(CorStunTime());
        }
        private IEnumerator CorStunTime()
        {
            yield return new WaitForSeconds(_parameters.stunTime);
            _isStunned = false;
            _agent.isStopped = false;
            _animator.SetBool(Stunned, false);
        }
        
        private void Prepare()
        {
            _parameters = ScriptableObject.CreateInstance<EnemyParameters>();
            _parameters = defaultParameters;
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _parameters.movementSpeed;
            _hearing = gameObject.GetComponent<EnemyHearing>();
            _vision = gameObject.GetComponent<EnemyVision>();
            _layerSwitcher = gameObject.GetComponent<LayerSwitcher>();
            _animator = GetComponentInChildren<Animator>();
            _hearing.Initialize(this);
            _vision.Initialize(this, _parameters, LayerMask.GetMask("Player"));
        }
    }
}
