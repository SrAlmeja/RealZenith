// Creado Raymundo Mosqueda 03/03/24


using com.LazyGames.DZ;
using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class TaskAttack : Node, IGeneralAggressor
    {
        private Transform _transform;
        private Animator _animator;
        private EnemyParameters _parameters;
        private Transform _target;
        
        private float _attackCounter;

        public TaskAttack(Transform transform, EnemyParameters parameters)
        {
            _transform = transform;
            _parameters = parameters;
            // _animator = transform.GetComponent<Animator>();
        }

        public override NodeState Evaluate()
        {
            _target = (Transform)GetData("target");
            _attackCounter += Time.fixedDeltaTime;
            if (_attackCounter >= _parameters.attackSpeed)
            {
                SendAggression();
                _attackCounter = 0;
            }
            
            state = NodeState.Running;
            return state;
        }

        public void SendAggression()
        {
            Debug.Log($"{_transform.gameObject.name} Attacked");
        // if(_attackCounter <= _parameters.attackSpeed) return;
        if (!_target.gameObject.TryGetComponent<IGeneralTarget>(out var generalTarget)) return;
        Debug.Log("Enemy attacked player");
        generalTarget.ReceiveAggression(Vector3.Normalize(_transform.position - _target.position), _parameters.attackPower);   
        }

    }
}
