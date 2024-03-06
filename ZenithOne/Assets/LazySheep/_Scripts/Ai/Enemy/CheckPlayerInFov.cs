// Creado Raymundo Mosqueda 02/03/24

using Obvious.Soap;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class CheckPlayerInFov : Node
    {
        private Transform _transform;
        private EnemyParameters _parameters;
        private LayerMask _playerLayerMask;

        private float _detectionMeter;
        private float _alertnessMultiplier = 1f;
        private Animator _animator;
        
        public CheckPlayerInFov(Transform transform, EnemyParameters parameters, LayerMask playerLayerMask)
        {
            _transform = transform;
            _parameters = parameters;
            // _animator = transform.GetComponent<Animator>();
            _playerLayerMask = playerLayerMask;
        }


        public override NodeStates Evaluate()
        {
            object t = GetData("target");
            if (t == null)
            {
                Collider[] colliders = Physics.OverlapSphere(_transform.position, _parameters.hardDetectionRange, _playerLayerMask);
                if(colliders.Length > 0)
                {
                    var targetPos = colliders[0].transform;
                    Vector3 targetDir = (targetPos.position - _transform.position).normalized;
                    if (Vector3.Angle(_transform.forward, targetDir) < _parameters.coneAngle / 2)
                    {
                        float dist = Vector3.Distance(_transform.position, targetPos.position);
                        if (Physics.Raycast(_transform.position, targetDir, out var hit, dist))
                        {
                            if (hit.collider.CompareTag("Player"))
                            {
                                Debug.Log("player detected");
                                parent.parent.SetData("target", targetPos);
                                state = NodeStates.Running;
                                return state;
                            }
                            
                            state = NodeStates.Failure;
                            return state;
                        }
                    }
                }
                state = NodeStates.Failure;
                return state;
            }
            state = NodeStates.Success;
            return state;
        }
    }
}


