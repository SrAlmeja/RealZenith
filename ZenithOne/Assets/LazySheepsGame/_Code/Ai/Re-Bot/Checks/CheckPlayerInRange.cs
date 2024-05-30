using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class CheckPlayerInRange : Node
    {
        private Transform _transform;
        private EnemyParameters _parameters;
        
        public CheckPlayerInRange(Transform transform, EnemyParameters parameters)
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
            
            Transform target = (Transform) t;
            float dist = Vector3.Distance(_transform.position, target.position);
            if (dist < _parameters.detectionRange)
            {
                state = NodeState.Success;
                return state;
            }
            
            state = NodeState.Running;
            return state;
        }
    }
}
