// Creado Raymundo Mosqueda 28/09/23

using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    [CreateAssetMenu(menuName = "LazySheeps/AI/EnemyParameters")]
    public class EnemyParameters : ScriptableObject
    { 
        [Header("Movement Variables")]
        [Tooltip("Time in seconds to wait at each waypoint")]
        public float waitTime = 1;
        public float patrolSpeed = 2f;
        public float alertSpeed = 4f;
        
        public float aggroSpeed = 6f;
        
        [Header("Detection Variables")]
        public float softDetectionRange = 20f;
        public float hardDetectionRange = 8f;
        public float coneAngle = 100f; 
        public Vector3 heightOffset = new Vector3(0, .5f, 0);
        
        [Header("Combat Variables")]
        public float maxHp = 20f;
        public float attackPower = 1f;
        public float attackRange = 2f;
        public float attackSpeed = 1.5f;
        
    }
}
