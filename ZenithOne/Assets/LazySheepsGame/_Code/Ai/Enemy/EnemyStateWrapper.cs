using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    [System.Serializable]
    public class EnemyStateWrapper 
    {
        public EnemyBt EnemyBt { get; set; }
        public EnemyState State { get; set; }
        
        public EnemyStateWrapper(EnemyBt enemyBt, EnemyState state = EnemyState.Patrolling)
        {
            EnemyBt = enemyBt;
            State = state;
        }
    }
}
