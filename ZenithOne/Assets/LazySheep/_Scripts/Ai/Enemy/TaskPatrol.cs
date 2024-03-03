// Creado Raymundo Mosqueda 19/02/24

using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.Dz.Ai
{
    public class TaskPatrol : Node
    {
        private Transform _transform;
        private Transform[] _wayPoints;
        private int _currentWayPoint;

        private float _waitCounter;
        private bool _waiting;
        
        private NavMeshAgent _agent;
        private Animator _animator;
        private EnemyParameters _parameters;
        
        public TaskPatrol(Transform transform, Transform[] wayPoints, EnemyParameters parameters)
        {
            _parameters = parameters;
            _transform = transform;
            _wayPoints = wayPoints;
            _agent = transform.GetComponent<NavMeshAgent>();
            // _animator = transform.GetComponent<Animator>(); 
            _agent.speed = _parameters.patrolSpeed;
            Debug.Log(parameters.waitTime);
        }
        
        public override NodeStates Evaluate()
        {
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= _parameters.waitTime)
                {
                    _waiting = false;
                }
            }
            else
            {
                Transform wp = _wayPoints[_currentWayPoint];
                if (Vector3.Distance(_transform.position, wp.position) < 0.6f)
                {
                    // _transform.position = wp.position;
                    _waitCounter = 0;
                    _waiting = true;
                    
                    // modulo loops through the waypoints
                    _currentWayPoint = (_currentWayPoint + 1) % _wayPoints.Length;
                }
                else
                {
                    _agent.SetDestination(wp.position);
                }
            }
            
            state = NodeStates.Running;
            return state;
        }
    }
}

