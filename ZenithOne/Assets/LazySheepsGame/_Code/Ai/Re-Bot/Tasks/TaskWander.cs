using UnityEngine;
using UnityEngine.AI;
using CryoStorage;

namespace com.LazyGames.Dz.Ai
{
    public class TaskWander : Node
    {
        private readonly Transform _transform;
        private readonly NavMeshAgent _agent;
        private readonly EnemyParameters _parameters;
       public TaskWander(Transform t, EnemyParameters parameters)
       {
           _transform = t;
            _parameters = parameters;
           _agent = t.GetComponent<NavMeshAgent>();
       }

       public override NodeState Evaluate(bool overrideStop = false)
       {
           var wanderForce = CryoMath.CryoSteering.Wander(_transform.position, _agent.velocity, 1f, 1f, 1f, 1f, _parameters.movementSpeed );
           _agent.SetDestination(wanderForce);
       }
    }
}
