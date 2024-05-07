// Creado Raymundo Mosqueda 19/02/24

using CryoStorage;
using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.Dz.Ai
{
    public class TaskPatrol : Node
    {
        private Transform _transform;
        private Waypoint[] _wayPoints;
        private int _currentWayPoint;

        private float _waitCounter;
        private bool _waiting;
        
        private NavMeshAgent _agent;
        private Animator _animator;
        private EnemyParameters _parameters;
        private float _waitTime;
        private Vector3 _lookPos;

        public TaskPatrol(Transform transform, Waypoint[] wayPoints, EnemyParameters parameters)
        {
            _parameters = parameters;
            _transform = transform;
            _wayPoints = wayPoints;
            _agent = transform.GetComponent<NavMeshAgent>();
            _agent.speed = _parameters.movementSpeed;
            _waitTime = _wayPoints[_currentWayPoint].WaitTime;
        }
        
        public override NodeState Evaluate(bool overrideStop = false)
        {
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                var pos = _transform.position;
                // _transform.LookAt(_lookPos);
                // Debug.DrawLine( pos, _lookPos, Color.red);
                if (_waitCounter >= _waitTime)
                {
                    _waiting = false;
                }
            }
            else
            {
                Transform wp = _wayPoints[_currentWayPoint].transform;
                if (Vector3.Distance(_transform.position, wp.position) < 0.6f)
                {
                    _waitCounter = 0;
                    _waiting = true;
                    _waitTime = _wayPoints[_currentWayPoint].WaitTime;
                    Vector3 lp = _wayPoints[_currentWayPoint].LookPosition ;
                    _lookPos = new Vector3(lp.x, lp.y, 0);
                    _currentWayPoint = (_currentWayPoint + 1) % _wayPoints.Length;

                }
                else
                {
                    _agent.SetDestination(wp.position);
                }
            }
            state = NodeState.Running;
            return state;
        }
    }
}

