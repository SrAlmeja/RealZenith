//Creado Raymundo Mosqueda 02/03/24

using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.Dz.Ai
{
    public class TaskGoToTarget : Node
    {
        private Transform _transform;
        private EnemyParameters _parameters;
        private NavMeshAgent _agent;
        
        public TaskGoToTarget(Transform transform, EnemyParameters parameters)
        {
            _transform = transform;
            _parameters = parameters;
            _agent = transform.GetComponent<NavMeshAgent>();
        }

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");
            Debug.Log(Vector3.Distance(_transform.position, target.position));

            if(Vector3.Distance(_transform.position, target.position) <= _parameters.attackRange)
            {
                Debug.Log("returning success");
                _agent.isStopped = true;
                state = NodeState.Success;
                return state;
            }
            
            _agent.isStopped = false;
            Debug.Log("evaluating go to target");
            _agent.SetDestination(target.transform.position);
            state = NodeState.Running;
            return state;
        }
        
    }
}
