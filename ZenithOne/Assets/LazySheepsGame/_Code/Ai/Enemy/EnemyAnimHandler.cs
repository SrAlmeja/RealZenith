using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace com.LazyGames.Dz.Ai
{
    public class EnemyAnimHandler : MonoBehaviour
    {
        private Animator _animator;
        private AnimatorOverrideController _controller;
        private EnemyBt _parentBt;

        public List<AnimationClip> anims;

        public void Initiate(Animator animator, EnemyBt parentBt)
        {
            _animator = animator;
            _parentBt = parentBt;
            _controller = new AnimatorOverrideController(animator.runtimeAnimatorController);
            _animator.runtimeAnimatorController = _controller;
            anims = _controller.animationClips.ToList();
        }
        
        void Update()
        {
            switch (_parentBt.State)
            {
                case EnemyState.Idle:
                    // _controller.
                    break;
                case EnemyState.Patrolling:
                    break;
                case EnemyState.Investigating:
                    break;
                case EnemyState.Chasing:
                    break;
                case EnemyState.Attacking:
                    break;
                case EnemyState.Stunned:
                    break;
            }
        }
    }
}
