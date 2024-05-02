using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.Dz.Ai
{
    public class TaskInvestigateNoise : Node
    {
        private Transform _transform;
        private NavMeshAgent _agent;
        private EnemyParameters _parameters;
        private float _waitCounter;
        
        public TaskInvestigateNoise(Transform transform, EnemyParameters parameters)
        {
            _transform = transform;
            _parameters = parameters;
            _agent = transform.GetComponent<NavMeshAgent>();
        }
        public override NodeState Evaluate(bool overrideStop = false)
        {
            object t = GetData("NoisePosition");
            if(t == null)
            {
                state = NodeState.Failure;
                return state;
            }
            Vector3 noisePosition = (Vector3) t;
            _agent.SetDestination(noisePosition);
            
            if(Vector3.Distance(_transform.position, noisePosition) < 2f)
            {
                if (_waitCounter < _parameters.searchTime)
                {
                    _waitCounter += Time.deltaTime;
                    state = NodeState.Running;
                    return state;
                }
                ClearData("NoisePosition"); 
                state = NodeState.Success;
                return state;
            }
            
            state = NodeState.Running;
            return state;
        }
    }
}
