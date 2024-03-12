using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.Dz.Ai
{
    public class TaskSearchLastKnownPosition : Node
    {
        private Transform _transform;
        private EnemyParameters _parameters;
        private NavMeshAgent _agent;
        private float _waitCounter;

        public TaskSearchLastKnownPosition(Transform transform, EnemyParameters parameters)
        {
            _transform = transform;
            _parameters = parameters;
            _agent = transform.GetComponent<NavMeshAgent>();
        }
        

        public override NodeState Evaluate()
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
            if(Vector3.Distance(_transform.position, lastKnownPosition) < 1f)
            {
                if (_waitCounter < _parameters.waitTime)
                {
                    _waitCounter += Time.deltaTime;
                    state = NodeState.Running;
                    return state;
                }
                
                ClearData("lastKnownPosition");
                parent.SetData("wary", true);
                state = NodeState.Failure;
                return state;
            }
            
            state = NodeState.Running;
            return state;

        }
    }
}
