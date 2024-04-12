using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class TaskPlayAnim : Node
    {
        private Transform _transform;
        private AnimationClip _clip;
        private Animator _animator;
        private float _waitTime;

        public TaskPlayAnim(Transform transform, Animator animator, AnimationClip clip)
        {
            _animator = animator;
            _clip = clip;
            _transform = transform;
        }
        
        public override NodeState Evaluate()
        {
            _animator.Play(_clip.name);
            if (!(_waitTime < _clip.length)) return NodeState.Success;
            _waitTime += Time.deltaTime;
            return NodeState.Running;
        }
        
    }
}
