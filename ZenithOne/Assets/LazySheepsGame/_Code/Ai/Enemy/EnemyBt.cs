using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    [SelectionBase]
    public class EnemyBt : Tree
    {
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private EnemyWayPoints enemyWayPoints;
        public EnemyParameters parameters;
        [HideInInspector] public float VisionAngle;
        
        
        protected override Node SetupTree()
        {
            Prepare();
            var t = transform;
            Node root = new Selector(new List<Node>
            { 
                new Sequence(new List<Node>
                {
                    new CheckCanSeeTarget(t, parameters),
                    new CheckPlayerInAttackRange(t, parameters),
                    new TaskAttack(t, parameters),
                }),
                new Sequence(new List<Node>
                {
                    new CheckPlayerInFov(t, parameters, playerLayer),
                    new TaskGoToTarget(t, parameters),
                }),
                new TaskSearchLastKnownPosition(t, parameters),
                new TaskPatrol(t, enemyWayPoints.WayPoints, parameters),
            });
            
            return root;
        }

        private void Prepare()
        {
            VisionAngle = parameters.coneAngle;
        }

    #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var position = transform.position + parameters.heightOffset;
            
            Handles.color = Color.white;
            Handles.DrawWireDisc(position, Vector3.up, parameters.hardDetectionRange);
            
            var eulerAngles = transform.eulerAngles;
            Vector3 viewAngle01 = DirFromAngle(eulerAngles.y, -parameters.coneAngle / 2);
            Vector3 viewAngle02 = DirFromAngle(eulerAngles.y, parameters.coneAngle / 2);
        
            Handles.color = Color.yellow;
            Handles.DrawLine(position, position + viewAngle01 * parameters.hardDetectionRange);
            Handles.DrawLine(position, position + viewAngle02 * parameters.hardDetectionRange);
            
            Vector3 viewAngle03 = DirFromAngle(eulerAngles.y, (-parameters.coneAngle -40f) / 2);
            Vector3 viewAngle04 = DirFromAngle(eulerAngles.y, (parameters.coneAngle + 40f)/ 2);
            
            Handles.color = Color.red;
            Handles.DrawLine(position, position + viewAngle03 * parameters.hardDetectionRange);
            Handles.DrawLine(position, position + viewAngle04 * parameters.hardDetectionRange);
                
            
        }
    #endif
        
        private Vector3 DirFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}
