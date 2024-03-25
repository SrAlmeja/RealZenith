using System;
using UnityEditor;
using UnityEngine;
using CryoStorage;

namespace com.LazyGames.Dz.Ai
{
    public class Waypoint : MonoBehaviour
    {
        public float WaitTime {get => waitTime; set => waitTime = value; }
        public Vector3 Pos { get; set; }
        public Vector3 LookPosition{ get; private set; }
        
        [SerializeReference] private float waitTime = 1;

        private void Start()
        {
            Pos = transform.position;
            LookPosition = GetLookPos();
        }

        private Vector3 GetLookPos()
        {
            var t = transform;
            var forwardVector = new Vector2(t.forward.x, t.forward.z);
            var lookAngle = CryoMath.AngleFromOffset(forwardVector);
            var lookTarget = CryoMath.PointOnRadius(transform.position, .5f , lookAngle);
            return lookTarget;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var t = transform;
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(t.position, 0.1f);
            Gizmos.color = Color.red;
            Handles.DrawWireDisc(t.position, Vector3.up, .5f);
            var forwardVector = new Vector2(t.forward.x, t.forward.z);
            var lookAngle = CryoMath.AngleFromOffset(forwardVector);
            
            var lookTarget = CryoMath.PointOnRadius(transform.position, .5f , lookAngle);
            Gizmos.DrawSphere(lookTarget, 0.1f);
            LookPosition = lookTarget;
        }
        
#endif
    }
}
