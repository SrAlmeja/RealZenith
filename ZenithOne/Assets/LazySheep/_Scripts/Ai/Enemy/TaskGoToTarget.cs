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

        public override NodeStates Evaluate()
        {

            Transform target = (Transform)GetData("target");
            if(Vector3.Distance(_transform.position, target.position) < _parameters.attackRange)
            {
                state = NodeStates.Success;
                return state;
            }

            _agent.SetDestination(target.position);
            state = NodeStates.Running;
            return state;
        }
        
    }
}
