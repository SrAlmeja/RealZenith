// Creado Raymundo Mosqueda 28/09/23

using UnityEngine;
using UnityEngine.Serialization;

namespace com.LazyGames.Dz.Ai
{
    [CreateAssetMenu(menuName = "LazySheeps/AI/EnemyParameters")]
    public class EnemyParameters : ScriptableObject
    { 
        [Header("Movement Variables")]
        [Tooltip("Time in seconds the enemy will spend investigating a noise or player sighting.")] 
        public float searchTime = 5;
        [Tooltip("Time in seconds will remain alert after losing sight of the player.")]
        public float alertTime = 10;
        public float movementSpeed = 2f;
        
        [Header("Detection Variables")]
        public float detectionRange = 8f;
        public float coneAngle = 100f; 
        public Vector3 heightOffset = new Vector3(0, .5f, 0);
        
        [Header("Combat Variables")]
        public float attackPower = 1f;
        public float attackRange = 2f;
        public float attackSpeed = 1.5f;
    }
}
