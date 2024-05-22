using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.Dz.Ai
{
    public class TaskSearchLastKnownPosition : Node
    {
        private readonly Transform _transform;
        private readonly EnemyParameters _parameters;
        private readonly NavMeshAgent _agent;
        private readonly Animator _animator;
        private float _waitCounter;
        private static readonly int Chasing = Animator.StringToHash("chasing");
        private static readonly int Alert = Animator.StringToHash("alert");

        public TaskSearchLastKnownPosition(Transform transform, EnemyParameters parameters)
        {
            _transform = transform;
            _parameters = parameters;
            _agent = transform.GetComponent<NavMeshAgent>();
            _animator = transform.GetComponentInChildren<Animator>();
        }

        public override NodeState Evaluate(bool overrideStop = false)
        {
            object pos = GetData("lastKnownPosition");
            if (pos == null)
            {
                state = NodeState.Failure;
                return state;
            }
            
            Vector3 lastKnownPosition = (Vector3) pos;
            _agent.SetDestination(lastKnownPosition);
            Debug.DrawLine(_transform.position, lastKnownPosition, Color.blue);
            if(Vector3.Distance(_transform.position, lastKnownPosition) < 2f)
            {
                if (_waitCounter < _parameters.searchTime)
                {
                    _animator.Play("enemy_look_around");
                    _waitCounter += Time.deltaTime;
                    state = NodeState.Running;
                    return state;
                }
                
                ClearData("lastKnownPosition");
                _animator.SetBool(Chasing, false);
                _animator.SetBool(Alert, true );
                
                parent.SetData("wary", true);
                state = NodeState.Failure;
                return state;
            }
            
            
            state = NodeState.Running;
            return state;

        }
    }
}
