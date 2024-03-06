using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class LaserTrap : TrapsBase
{
    [SerializeField] private ScriptableEvent<string> laserCollisionEvent;
    [SerializeField] private Collider laserCollider;
    [SerializeField] private GameObject laserObject;
    [SerializeField] private GameObject boxVisual;
    
    
    
    public override void ActivateTrap()
    {
        base.ActivateTrap();
        laserCollisionEvent.OnRaised += LaserCollisionEvent;
        laserObject.SetActive(true);
        boxVisual.SetActive(true);
        
    }
    public override void DeactivateTrap()
    {
        base.DeactivateTrap();
        laserCollisionEvent.OnRaised -= LaserCollisionEvent;
        laserObject.SetActive(false);
    }
    public override void ResetTrap()
    {
        base.ResetTrap();
        
    }
    public override void TriggerTrap()
    {
        base.TriggerTrap();
    }
    public override void DestroyTrap()
    {
        base.DestroyTrap();
        laserCollisionEvent.OnRaised -= LaserCollisionEvent;
        laserObject.SetActive(false);
        boxVisual.SetActive(false);
    }
    public override void DisableTrap()
    {
        base.DisableTrap();
        laserObject.SetActive(false);
        
    }

    private void StartTimer()
    {
        
    }
    
    private void LaserCollisionEvent(string message)
    {
        if (message == "Player Hit by Laser")
        {
            Debug.Log("Player Hit by Laser");
            
        }
    }
}
