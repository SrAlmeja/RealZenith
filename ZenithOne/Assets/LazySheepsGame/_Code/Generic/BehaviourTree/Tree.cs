using System;
using System.Collections;
using UnityEngine;


namespace com.LazyGames.Dz.Ai
{
   
        public abstract class Tree : MonoBehaviour
        {
            [SerializeField] private float tickRate = 0.5f;
            private Node ActiveNode { get; set; }
            private Node _root = null;

            protected void Start()
            {
                _root = SetupTree();
                ActiveNode = _root;
                // StartCoroutine(CorTick());
            }
            
            private IEnumerator CorTick()
            {
                while (true)
                {
                    ActiveNode.Evaluate();
                    yield return new WaitForSeconds(tickRate);
                }
            }

            // private void Update()
            // {
            //     if (ActiveNode != null)
            //         ActiveNode.Evaluate();
            // }

            protected abstract Node SetupTree();

        }
}
