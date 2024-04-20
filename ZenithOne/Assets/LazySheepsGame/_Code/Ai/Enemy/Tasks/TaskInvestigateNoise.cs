using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.Dz.Ai
{
    public class TaskInvestigateNoise : Node
    {
        private Transform _transform;
        private NavMeshAgent _agent;

        public TaskInvestigateNoise(Transform transform)
        {
            _transform = transform;
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
                ClearData("NoisePosition"); 
                state = NodeState.Success;
                return state;
            }
            
            state = NodeState.Running;
            return state;
        }
    }
}
