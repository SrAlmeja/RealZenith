using UnityEditor;
using UnityEngine;
using CryoStorage;

namespace com.LazyGames.Dz.Ai
{
    [SelectionBase]
    public class Waypoint : MonoBehaviour
    {
        public float waitTime = 0.3f;
        public Vector3 LookTarget{ get; set; }


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
            LookTarget = lookTarget;
        }
    }
}
