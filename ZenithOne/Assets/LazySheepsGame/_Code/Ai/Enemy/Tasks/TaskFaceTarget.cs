//Creado Raymundo Mosqueda 19/04/24

using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class TaskFaceTarget : Node
    {
        private readonly Transform _transform;
        private readonly EnemyParameters _parameters;

        
        private Quaternion _targetRotation;
        
        public TaskFaceTarget(Transform transform, EnemyParameters parameters)
        {
            _transform = transform;
            _parameters = parameters;
        }
        
        public override NodeState Evaluate(bool overrideStop = false)
        {
             object t = GetData("target");
             if (t == null)
             {
                 state = NodeState.Failure;
                 return state;
             }
             var target = (Transform)t;
             var targetPlanar = new Vector3(target.position.x, _transform.position.y, target.position.z);
             var targetDir = (targetPlanar - _transform.position).normalized;
             
             _targetRotation = Quaternion.LookRotation(targetDir);
             _transform.rotation = Quaternion.Slerp(_transform.rotation, _targetRotation, Time.deltaTime *5);
             
             state = NodeState.Running;
             return state;
        }
        
    }
}
