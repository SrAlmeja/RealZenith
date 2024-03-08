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
    
    TimerBase _timer;
    
    protected override void ActivateTrap()
    {
        base.ActivateTrap();
        
        laserCollisionEvent.OnRaised += LaserCollisionEvent;
        
        laserObject.SetActive(true);
        boxVisual.SetActive(true);
        _timer = gameObject.AddComponent<TimerBase>();
        _timer.OnTimerEnd += () => EnableLaser(false);
        _timer.OnTimerLoop += () => EnableLaser(true);
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
        _timer.OnTimerEnd -= () => EnableLaser(false);
        _timer.OnTimerLoop -= () => EnableLaser(true);
        Destroy(_timer);
        
    }
    protected override void DisableTrap()
    {
        base.DisableTrap();
        laserObject.SetActive(false);
    }
    
    private void StartTimer()
    {
        Debug.Log("Laser Trap Started");
        _timer.SetLoopableTimer(interludeTime, false, 1f, "Laser Trap");
        
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
    
   
   
}
