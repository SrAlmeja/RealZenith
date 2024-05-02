using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public class EnemyAnimHandler : MonoBehaviour
    {
        
        private Animator _animator;
        private EnemyBt _parentBt;
        private int _animIndex;
        
        [SerializeField] private AnimationsList anims;
        public void Initialize(Animator animator, EnemyBt parentBt)
        {
            _parentBt = parentBt;
            _animator = animator;
            Debug.Log($"initialized");
        }

        public void NextAnim()
        {
            var len = anims.animations.Count;
            _animIndex = (_animIndex + 1) % len;
            PlayNextAnim();
        }

        private void PlayNextAnim()
        {
            var controller = _animator.runtimeAnimatorController as AnimatorController;
            var rootStateMachine = controller.layers[0].stateMachine;
            foreach (var state in rootStateMachine.states)
            {
                // _animator.CrossFade(anims.animations[_animIndex].name, .02f);
                _animIndex = (_animIndex + 1) % anims.animations.Count;
                state.state.motion = anims.animations[_animIndex];
                break;
            }
        }
    }
    #if UNITY_EDITOR
    [CustomEditor(typeof(EnemyAnimHandler))]
    public class EnemyAnimHandlerEditor : Editor
    {
        private EnemyAnimHandler _target;

        private void OnEnable()
        {
            _target = (EnemyAnimHandler)target;
        }
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Change Anim"))
            {
                _target.NextAnim();
            }
            base.OnInspectorGUI();
        }
    }
    #endif
}
