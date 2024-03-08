using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using UnityEngine;

public class LaserTrap : TrapsBase
{
    [SerializeField] private ScriptableEvent<string> laserCollisionEvent;
    [SerializeField] private Collider laserCollider;
    [SerializeField] private GameObject laserObject;
    [SerializeField] private GameObject boxVisual;
    [SerializeField] private float interludeTime;
    [SerializeField] private float deactivateTime;
   
    private TimerBase _interludeTimer;
    private TimerBase _deactivateTimer;
    
    
    
    protected override void ActivateTrap()
    {
        base.ActivateTrap();
        
        Debug.Log("Laser Trap Activated! = ".SetColor("#FED744") + gameObject.name);
        laserCollisionEvent.OnRaised += LaserCollisionEvent;
        
        laserObject.SetActive(true);
        boxVisual.SetActive(true);
        
        _interludeTimer = gameObject.AddComponent<TimerBase>();
        _deactivateTimer = gameObject.AddComponent<TimerBase>();
        _deactivateTimer.OnTimerEnd += StartTimer;
        
        StartTimer();
        
    }
    protected override void DeactivateTrap()
    {
        base.DeactivateTrap();
        laserCollisionEvent.OnRaised -= LaserCollisionEvent;
        laserObject.SetActive(false);
    }
    protected override void ResetTrap()
    {
        base.ResetTrap();
        
    }
    protected override void TriggerTrap()
    {
        base.TriggerTrap();
    }
    protected override void DestroyTrap()
    {
        base.DestroyTrap();
        laserCollisionEvent.OnRaised -= LaserCollisionEvent;
        laserObject.SetActive(false);
        boxVisual.SetActive(false);
    }
    protected override void DisableTrap()
    {
        base.DisableTrap();
        laserObject.SetActive(false);
        
    }
    
    private void StartTimer()
    {
        _interludeTimer.SetLoopableTimer(interludeTime, false, interludeTime, "Interlude Timer");
        UpdateLaser();
        
        
    }
    
    private void LaserCollisionEvent(string message)
    {
        if (message == "Player Hit by Laser")
        {
            Debug.Log("Player Hit by Laser");
            
        }
    }
    private void  EnableLaser( bool enable)
    {
        laserObject.SetActive(enable);
    }
    private void UpdateLaser()
    {
        EnableLaser(false);
        _interludeTimer.PauseTimer();
        _deactivateTimer.SetLoopableTimer(deactivateTime, false, deactivateTime, "Deactivate Timer");
        _deactivateTimer.OnTimerEnd += (() => { EnableLaser(true); });
    }
   
}
