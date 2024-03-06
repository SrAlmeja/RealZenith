using System;
using System.Collections;
using UnityEngine;


namespace com.LazyGames.Dz.Ai
{
    public abstract class Tree : MonoBehaviour
    {
        private Node ActiveNode { get; set; }

        public Node Root => _root;
        
        [SerializeField]protected float tickRate = 0.5f;
        
        private Node _root;

        protected void Start()
        {
            _root = SetupTree();
            ActiveNode = _root;
            // StartCoroutine(CorTick());
        }

        private void Update()
        {
            ActiveNode?.Evaluate();
        }

        private IEnumerator CorTick()
        {
            while (true)
            {
                yield return new WaitForSeconds(tickRate);
            }
        }
        
        public void SetActiveNode(Node node)
        {
            ActiveNode = node;
        }

        protected abstract Node SetupTree();
    }
}
