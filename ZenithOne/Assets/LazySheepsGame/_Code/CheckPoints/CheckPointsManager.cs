using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class CheckPointsManager : MonoBehaviour
{
   #region SerializedFields
   
   [SerializeField] private ScriptableEventNoParam playerDeathEvent;
   [SerializeField] List<CheckPoint> _checkPoints;

   // [SerializeField] private ScriptableEvent
   #endregion

   #region private variables

   private CheckPoint LastCheckPoint
   {
       get
       {
          if (_checkPointQueue.Count > 0)
          {
             return _checkPointQueue.Peek();
          }
          return null;
       }  
   }
   private Queue<CheckPoint> _checkPointQueue = new Queue<CheckPoint>();
   
   #endregion

   private void Start()
   {
      playerDeathEvent.OnRaised += MovePlayerLastCheckPoint;
   }

   private void MovePlayerLastCheckPoint()
   {
      if (LastCheckPoint != null)
      {
         // Move player to last checkpoint
         PlayerSpawn.Instance.MovePlayerToCheckPoint(LastCheckPoint.CheckPointPosition);
      }
   }
}
