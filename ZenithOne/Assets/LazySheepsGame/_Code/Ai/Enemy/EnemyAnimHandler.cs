using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace com.LazyGames.Dz.Ai
{
    public class EnemyAnimHandler : MonoBehaviour
    {
        [SerializeField]private AnimatorOverrideController aOController;
        
        private Animator _animator;
        private EnemyBt _parentBt;
        public List<KeyValuePair<AnimationClip, AnimationClip>> anims = new();

        public void Initiate(Animator animator, EnemyBt parentBt)
        {
            _parentBt = parentBt;
            
            aOController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            foreach (var a in aOController.animationClips)
            { 
                aOController.ApplyOverrides(anims);
            } 
            animator.runtimeAnimatorController = aOController;
            
            // animator.Play(anims[0].Value.name);
        }
        
        void Update()
        {
            // switch (_parentBt.State)
            // {
            //     case EnemyState.Idle:
            //         // _controller.
            //         break;
            //     case EnemyState.Patrolling:
            //         break;
            //     case EnemyState.Investigating:
            //         break;
            //     case EnemyState.Chasing:
            //         break;
            //     case EnemyState.Attacking:
            //         break;
            //     case EnemyState.Stunned:
            //         break;
            // }
        }
    }
}
