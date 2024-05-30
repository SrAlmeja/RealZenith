// Creado Raymundo Mosqueda 03/03/24


using com.LazyGames.DZ;
using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class TaskAttack : Node, IGeneralAggressor
    {
        private readonly Transform _transform;
        private readonly Animator _animator;
        private readonly EnemyParameters _parameters;
        private Transform _target;
        
        private float _attackCounter;
        private static readonly int Attacking = Animator.StringToHash("attacking");

        public TaskAttack(Transform transform, EnemyParameters parameters)
        {
            _transform = transform;
            _parameters = parameters;
            _animator = transform.GetComponentInChildren<Animator>();
        }

        public override NodeState Evaluate(bool overrideStop = false)
        {
            _target = (Transform)GetData("target");

            _attackCounter += Time.fixedDeltaTime;
            _animator.SetBool(Attacking, false);
            if (_attackCounter >= _parameters.attackSpeed)
            {
                // SendAggression();
                _attackCounter = 0;
                _animator.CrossFade("enemy_attack", 0.2f);
                _animator.SetBool(Attacking, true);
            }
            
            _animator.SetBool(Attacking, false);
            state = NodeState.Running;
            return state;
        }

        public void SendAggression()
        {
            if (!_target.gameObject.TryGetComponent<IGeneralTarget>(out var generalTarget)) return;
            generalTarget.ReceiveAggression(Vector3.Normalize(_transform.position - _target.position), _parameters.attackPower);   
        }

    }
}
