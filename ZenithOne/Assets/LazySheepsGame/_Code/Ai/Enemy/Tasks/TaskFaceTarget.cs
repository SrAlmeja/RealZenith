//Creado Raymundo Mosqueda 19/04/24

using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class TaskFaceTarget : Node
    {
        private readonly Transform _transform;
        private readonly EnemyParameters _parameters;
        
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

            if (!(Vector3.Angle(_transform.forward, targetDir) < _parameters.coneAngle / 2))
            {
                state = NodeState.Failure;
                return state;
            }
            _transform.rotation = Quaternion.LookRotation(targetDir);
            state = NodeState.Running;
            return state;
        }
        
    }
}
