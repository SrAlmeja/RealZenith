//Creado Raymundo Mosqueda 19/04/24

using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.Dz.Ai
{
    public class TaskFaceTarget : Node
    {
        private readonly Transform _transform;
        private readonly EnemyParameters _parameters;
        private readonly NavMeshAgent _agent;

        
        private Quaternion _targetRotation;
        
        public TaskFaceTarget(Transform transform, EnemyParameters parameters)
        {
            _transform = transform;
            _parameters = parameters;
            _agent = transform.GetComponent<NavMeshAgent>();
        }
        
        public override NodeState Evaluate(bool overrideStop = false)
        {
             var t = GetData("target");
             if (t == null)
             {
                 _agent.isStopped = false;
                 state = NodeState.Failure;
                 return state;
             }
             _agent.isStopped = true;
             var target = (Transform)t;
             var targetPlanar = new Vector3(target.position.x, _transform.position.y, target.position.z);
             var targetDir = (targetPlanar - _transform.position).normalized;
             
             _targetRotation = Quaternion.LookRotation(targetDir);
             _transform.rotation = Quaternion.Slerp(_transform.rotation, _targetRotation, Time.deltaTime * _parameters.rotationSpeed);
             
             state = NodeState.Running;
             return state;
        }
        
    }
}
