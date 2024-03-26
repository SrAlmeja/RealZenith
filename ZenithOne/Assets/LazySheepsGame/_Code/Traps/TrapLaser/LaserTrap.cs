using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class LaserTrap : TrapsBase, ITrapInteraction
{
    #region Serialized fields

    [Header("Laser Trap")]
    [SerializeField] private ScriptableEvent<string> laserCollisionEvent;
    [SerializeField] private ScriptableEvent<Vector3> playerPositionEvent;
    [SerializeField] private ScriptableEvent<float> playerReceivedDamageEvent;
    [SerializeField] private Collider laserCollider;
    [SerializeField] private GameObject laserObject;
    [SerializeField] private GameObject boxVisual;
    [SerializeField] private float interludeTime;

    [Header("Functionality")]
    [SerializeField] private bool NeedsTimer;
    
    [Header("Trap Interaction")]
    [SerializeField] private ITrapInteraction _gadgetInteractionType;

    #endregion

    #region public methods
    public void SetATrapActive()
    {
        ActivateTrap();
    }
    public void SetDeactiveTrap()
    {
        DeactivateTrap();
    }
    #endregion

    #region ITrapInteraction

    public TypeOfGadget gadgetType
    {
        get => _gadgetInteractionType.gadgetType;
        set => _gadgetInteractionType.gadgetType = value;
    }

    public void DamagePlayer(float dmg)
    {
    }

    void ITrapInteraction.DestroyTrap(TypeOfGadget gadgetType)
    {
        if(gadgetType != _gadgetInteractionType.gadgetType) return;
        DestroyTrap();
    }

    void ITrapInteraction.DisableTrap(TypeOfGadget gadgetType)
    {
        if(gadgetType != _gadgetInteractionType.gadgetType) return;
        DisableTrap();
    }

    public void EnableTrap()
    {
        ActivateTrap();
    }
    #endregion
    
    #region private methods

    protected override void ActivateTrap()
    {
        base.ActivateTrap();
        
        laserCollisionEvent.OnRaised += LaserCollisionEvent;
        
        laserObject.SetActive(true);
        boxVisual.SetActive(true);
       
        if(NeedsTimer)
            StartTimer();
        
    }
    
    protected override void DeactivateTrap()
    {
        base.DeactivateTrap();
        laserCollisionEvent.OnRaised -= LaserCollisionEvent;
        laserObject.SetActive(false);
        StopAllCoroutines();
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
        StopAllCoroutines();
        
    }
    protected override void DisableTrap()
    {
        base.DisableTrap();
        laserObject.SetActive(false);
        
    }
    
    private void StartTimer()
    {
        // Debug.Log("Laser Trap Started");
        StartCoroutine(InitTimer());

    }
    
    private void LaserCollisionEvent(string message)
    {
        if (message == "Player Hit by Laser")
        {
            // playerPositionEvent.Raise(transform.position);
            // playerReceivedDamageEvent.Raise(50);
        }
    }
    private void  EnableLaser( bool enable)
    {
        laserObject.SetActive(enable);
    }

    IEnumerator InitTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(interludeTime);
            EnableLaser(!laserObject.activeSelf);
        }
    }
    #endregion

}
#if UNITY_EDITOR_WIN
[CustomEditor(typeof(LaserTrap))]
public class LaserTrapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LaserTrap laserTrap = (LaserTrap) target;
        if (GUILayout.Button("Activate Trap"))
        {
            laserTrap.SetATrapActive();
        }

        if (GUILayout.Button("Deactivate Trap"))
        {
            laserTrap.SetDeactiveTrap();
        }
    }
    
}

#endif