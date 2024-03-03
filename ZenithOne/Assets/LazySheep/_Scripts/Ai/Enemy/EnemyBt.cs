using UnityEngine;
namespace com.LazyGames.Dz.Ai
{
    [SelectionBase]
    public class EnemyBt : Tree
    { 
        [SerializeField] private EnemyWayPoints enemyWayPoints;
        [SerializeField] private EnemyParameters parameters;

        protected override Node SetupTree()
        {
            Node root = new TaskPatrol(transform, enemyWayPoints.WayPoints, parameters);
            return root;
        }
    }
}
