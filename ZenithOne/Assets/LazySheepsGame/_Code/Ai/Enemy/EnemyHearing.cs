using com.LazyGames.DZ;
using com.LazyGames.Dz.Ai;
using UnityEngine;

public class EnemyHearing : MonoBehaviour, INoiseSensitive
{
   private EnemyBt _parentBt;
   public void Initialize(EnemyBt parentBt)
   {
      _parentBt = parentBt;
   }
   public void HearNoise(float intensity, Vector3 position, bool dangerous)
   {
      _parentBt.NoiseHeard(position);
   }
}
