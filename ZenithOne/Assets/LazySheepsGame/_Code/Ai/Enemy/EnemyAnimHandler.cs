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
            PlayAnim();
        }

        private void PlayAnim()
        {
            AnimatorController controller = _animator.runtimeAnimatorController as AnimatorController;
            AnimatorStateMachine rootStateMachine = controller.layers[0].stateMachine;
            foreach (ChildAnimatorState state in rootStateMachine.states)
            {
                // _animIndex = (_animIndex + 1) % anims.animations.Count;
                _animator.CrossFade(anims.animations[_animIndex].name, .02f);
                state.state.motion = anims.animations[_animIndex];
                break;
            }
        }

        // void Update()
        // {
        //     switch (_parentBt.State)
        //     {
        //         case EnemyState.Idle:
        //             break;
        //         case EnemyState.Patrolling:
        //             Debug.Log("playing anim");
        //             _animator.CrossFade(anims[0].Key.name, 0.2f);
        //             break;
        //         case EnemyState.Investigating:
        //             break;
        //         case EnemyState.Chasing:
        //             break;
        //         case EnemyState.Attacking:
        //             break;
        //         case EnemyState.Stunned:
        //             break;
        //     }
        // }
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
