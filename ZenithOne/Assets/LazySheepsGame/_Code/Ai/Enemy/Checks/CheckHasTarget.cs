// Creado Raymundo Mosqueda 02/03/24

using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class CheckHasTarget : Node
    {
        private Transform _transform;
        private float _visionAngle;
        private float _visionRange;
        private LayerMask _playerLayerMask;
        private EnemyParameters _parameters;

        private float _detectionMeter;
        // private float _alertnessMultiplier = 1f;
        private Animator _animator;
        
        public CheckHasTarget(EnemyParameters parameters)
        {
            _visionAngle = parameters.coneAngle;
            _visionRange = parameters.detectionRange;
        }


        public override NodeState Evaluate(bool overrideStop = false)
        {
            object wary = GetData("wary");
            if (wary != null)
            {
                bool waryBool = (bool)wary;
                float previousAngle = _visionAngle;
                if (waryBool)
                {
                    _visionAngle += 20;
                }
                else
                {
                    _visionAngle = previousAngle;
                }
            }

            object t = GetData("target");
            if (t == null)
            {
                state = NodeState.Failure;
                return state;
            }
            state = NodeState.Success;
            return state;
        }
    }
}


