using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using UnityEngine;

public class CheckPointsManager : MonoBehaviour
{
   #region SerializedFields
   
   [SerializeField] private ScriptableEventNoParam playerDeathEvent;
   [SerializeField] private ScriptableEventCheckPoint _checkPointEvent;

   #endregion

   #region private variables

   private CheckPoint LastCheckPoint
   {
       get
       {
          if (_checkPointStack.Count > 0)
          {
             return _checkPointStack.Peek();
          }
          return null;
       }  
   }
   private Stack<CheckPoint> _checkPointStack = new Stack<CheckPoint>();
   
   #endregion

   #region unity methods
   
   private void Start()
   {
      playerDeathEvent.OnRaised += MovePlayerLastCheckPoint;
      _checkPointEvent.OnRaised += AddCheckPoint; 
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.P))
      {
         MovePlayerLastCheckPoint();
      }
   }

   private void OnDisable()
   {
      playerDeathEvent.OnRaised -= MovePlayerLastCheckPoint;
      _checkPointEvent.OnRaised -= AddCheckPoint;
   }
   #endregion

   #region private methods
   
   private void MovePlayerLastCheckPoint()
   {
      if (LastCheckPoint != null)
      {
         // Move player to last checkpoint
         PlayerSpawn.Instance.MovePlayerToCheckPoint(LastCheckPoint.CheckPointPosition);
         // Debug.Log("Player moved to last checkpoint = ".SetColor("#FED744") +  LastCheckPoint.name);
        
      }
   }
   
   private void AddCheckPoint(CheckPoint checkPoint)
   {
      _checkPointStack.Push(checkPoint);
   }
   #endregion

}
