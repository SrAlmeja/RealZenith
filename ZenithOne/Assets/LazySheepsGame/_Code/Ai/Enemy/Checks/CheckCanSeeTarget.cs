// Creado Raymundo Mosqueda 07/03/24

using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class CheckCanSeeTarget : Node
    {
        private Transform _transform;
        private EnemyParameters _parameters;
        
        public CheckCanSeeTarget(Transform transform, EnemyParameters parameters)
        {
            _transform = transform;
            _parameters = parameters;
        }
        
        public override NodeState Evaluate()
        {
            object t = GetData("target");
            if (t == null)
            {
                state = NodeState.Failure;
                return state;
            }
            
            Transform target = (Transform) t;
            Vector3 viewPos = _transform.position + _parameters.heightOffset;
            Vector3 targetDir = (target.position - viewPos).normalized;
            float dist = Vector3.Distance(viewPos, target.position);
            Debug.DrawRay(viewPos, targetDir * dist, Color.red);
            if(Physics.Raycast(_transform.position + _parameters.heightOffset, targetDir, out var hit, dist))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    state = NodeState.Success;
                    return state;
                }
            }
            
            parent.parent.SetData("lastKnownPosition", target.position);
            Debug.Log("creating last known position");
            ClearData("target");
            state = NodeState.Failure;
            return state;
        }
    }
}
