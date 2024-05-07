//Creado Raymundo Mosqueda 19/04/24

using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class TaskLookAt : Node
    {
        private readonly Transform _transform;
        
        public TaskLookAt(Transform transform)
        {
            _transform = transform;
        }
        
        public override NodeState Evaluate(bool overrideStop = false)
        {
            object t = GetData("target");
            if (t == null)
            {
                state = NodeState.Failure;
                return state;
            }
            var lookPos = (Transform)t;
            var dot = Vector3.Dot(_transform.forward, lookPos.position);
            if (dot > 0.99f)
            {
                state = NodeState.Running;
                return state;
            }
            _transform.LookAt(lookPos);
            state = NodeState.Running;
            return state;
        }
        
    }
}
