// Creado Raymundo Mosqueda 03/03/24

using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class CheckPlayerInAttackRange : Node
    {
        private Transform _transform;
        private Animator _animator;
        private EnemyParameters _parameters;
        
        public  CheckPlayerInAttackRange(Transform transform, EnemyParameters parameters)
        {
            _transform = transform;
            _parameters = parameters;
            // _animator = transform.GetComponent<Animator>();
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
            // Debug.Log($"attackRange: {_parameters.attackRange}, Dist: {Vector3.Distance(_transform.position, target.position)}");
            if (Vector3.Distance(_transform.position, target.position) <= _parameters.attackRange)
            {
                state = NodeState.Success;
                return state;
            }
            state = NodeState.Failure;
            return state;
        }
    }
}
