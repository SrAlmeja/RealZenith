using System.Collections;
using com.LazyGames.Dz.Ai;
using UnityEngine;


namespace com.LazyGames.Dz.Ai
{
    public class AnimationAttack : MonoBehaviour
    {
        public bool isAttacking;
        
        [SerializeField] private EnemyBt bt;

        private Node _attackNode;
        
        
        public void OnApplyDamage()
        {
            isAttacking = true;
            StartCoroutine(CorAttackReset());
            // _attackNode = bt.attackNode;
            // ((TaskAttack)_attackNode).SendAggression();
        }

        private IEnumerator CorAttackReset()
        {
            yield return new WaitForSeconds(1);
            isAttacking = false;
        }

    }
}
