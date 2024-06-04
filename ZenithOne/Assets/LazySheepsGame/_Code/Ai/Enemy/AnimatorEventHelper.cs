using System;
using System.Collections;
using UnityEngine;
using FMODUnity;

namespace com.LazyGames.Dz.Ai
{
    public class AnimatorEventHelper : MonoBehaviour
    {
        public bool isAttacking;
        
        [SerializeField] private EnemyBt bt;

        private Node _attackNode;
        [SerializeField]private StudioEventEmitter stepAudioEmitter;
        [SerializeField]private StudioEventEmitter attackAudioEmitter;

        private void Awake()
        {
            stepAudioEmitter = GetComponent<StudioEventEmitter>();
        }


        public void OnApplyDamage()
        {
            attackAudioEmitter.Play();
            isAttacking = true;
            StartCoroutine(CorAttackReset());
        }

        private IEnumerator CorAttackReset()
        {
            yield return new WaitForSeconds(1);
            isAttacking = false;
        }

        public void OnStep()
        {
            stepAudioEmitter.Play();
        }
        
        

    }
}
