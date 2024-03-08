using System;
using System.Collections.Generic;
using Obvious.Soap;
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
        
        protected override Node SetupTree()
        {
            var t = transform;
            Node root = new Selector(new List<Node>
            { 
                new Sequence(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new CheckCanSeeTarget(t, parameters),
                        new CheckPlayerInAttackRange(t, parameters),
                        new TaskAttack(t, parameters),
                    })
                }),
                new Sequence(new List<Node>
                {
                    new CheckPlayerInFov(t, parameters, playerLayer),
                    new TaskGoToTarget(t, parameters),
                }),
                new TaskPatrol(t, enemyWayPoints.WayPoints, parameters),
            });
            
            return root;
        }

        private void OnDrawGizmos()
        {
            bool hasTarget = false;
            var position = transform.position + parameters.heightOffset;

            if (hasTarget)
            {
                
                Handles.color = Color.green;
                Handles.DrawWireDisc(position, Vector3.up, parameters.hardDetectionRange);
                
                var eulerAngles = transform.eulerAngles;
                Vector3 viewAngle01 = DirFromAngle(eulerAngles.y, -parameters.coneAngle / 2);
                Vector3 viewAngle02 = DirFromAngle(eulerAngles.y, parameters.coneAngle / 2);
            
                Handles.color = Color.yellow;
                Handles.DrawLine(position, position + viewAngle01 * parameters.hardDetectionRange);
                Handles.DrawLine(position, position + viewAngle02 * parameters.hardDetectionRange);
            }
            else
            {
                Handles.color = Color.red;
                Handles.DrawWireDisc(position, Vector3.up, parameters.hardDetectionRange);
                
                var eulerAngles = transform.eulerAngles;
                Vector3 viewAngle01 = DirFromAngle(eulerAngles.y, -parameters.coneAngle / 2);
                Vector3 viewAngle02 = DirFromAngle(eulerAngles.y, parameters.coneAngle / 2);
            
                Handles.color = Color.yellow;
                Handles.DrawLine(position, position + viewAngle01 * parameters.hardDetectionRange);
                Handles.DrawLine(position, position + viewAngle02 * parameters.hardDetectionRange);
            }
        }
        
        private Vector3 DirFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}
