// Creado Raymundo Mosqueda 02/03/24

using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class CheckPlayerInFov : Node
    {
        private Transform _transform;
        private float _visionAngle;
        private float _visionRange;
        private LayerMask _playerLayerMask;
        private EnemyParameters _parameters;

        private float _detectionMeter;
        // private float _alertnessMultiplier = 1f;
        private Animator _animator;
        
        public CheckPlayerInFov(Transform transform, EnemyParameters parameters, LayerMask playerLayerMask)
        {
            _transform = transform;
            _visionAngle = parameters.coneAngle;
            _visionRange = parameters.hardDetectionRange;
            // _animator = transform.GetComponent<Animator>();
            _playerLayerMask = playerLayerMask;
        }


        public override NodeState Evaluate()
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
                Collider[] colliders = Physics.OverlapSphere(_transform.position, _visionRange, _playerLayerMask);
                if(colliders.Length > 0)
                {
                    var targetPos = colliders[0].transform;
                    Vector3 targetDir = (targetPos.position - _transform.position).normalized;
                    if (Vector3.Angle(_transform.forward, targetDir) < _visionAngle / 2)
                    {
                        float dist = Vector3.Distance(_transform.position, targetPos.position);
                        if (Physics.Raycast(_transform.position, targetDir, out var hit, dist))
                        {
                            if (hit.collider.CompareTag("Player"))
                            {
                                Debug.Log("player detected");
                                parent.parent.SetData("target", targetPos);
                                state = NodeState.Running;
                                return state;
                            }
                            
                            state = NodeState.Failure;
                            return state;
                        }
                    }
                }
                state = NodeState.Failure;
                return state;
            }
            state = NodeState.Success;
            return state;
        }
    }
}


