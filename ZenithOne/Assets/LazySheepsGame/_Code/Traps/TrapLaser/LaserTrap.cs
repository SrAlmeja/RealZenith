using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using DG.Tweening;
using FMODUnity;
using Obvious.Soap;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class LaserTrap : TrapsBase, IGadgetInteractable
{
    #region Serialized fields

    [Header("Laser Trap")]
    [SerializeField] private ScriptableEvent<Vector3> playerPositionEvent;
    [SerializeField] private ScriptableEvent<float> playerReceivedDamageEvent;
    [SerializeField] private GameObject laserObject;
    [SerializeField] private GameObject boxVisual;
    [SerializeField] private GameObject railVisual; 
    [SerializeField] private float interludeTime;
    [SerializeField] private float speedMovement = 2.5f;
    [SerializeField] private GameObject deactivateParticles;
    
    [SerializeField] private LaserCollision laserCollision;

    [Header("Functionality")]
    [SerializeField] private bool NeedsTimer;
    [SerializeField] private Transform laserMovPosition;
    
    [Header("Trap Interaction")]
    [SerializeField] private TypeOfGadget _gadgetInteractionType;
    
    [Header("Sounds")]
    [SerializeField] private StudioEventEmitter laserSound;
    [SerializeField] private StudioEventEmitter deactivateSound;

    [SerializeField]
    private bool canmove = true;
    
    public UnityEvent onTrapCompletedMovement;

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
        get => _gadgetInteractionType;
        set => _gadgetInteractionType = value;
    }

    public void DamagePlayer(float dmg)
    {
    }
    
    public void EnableTrap()
    {
        ActivateTrap();
    }
    #endregion
    
    #region private methods

    protected override void ActivateTrap()
    {
        if(speedMovement == 0) canmove = false;
       
        base.ActivateTrap();
        deactivateParticles.SetActive(false);
        laserObject.SetActive(true);
        boxVisual.SetActive(true);
        laserSound.Play();
        
        if(NeedsTimer) StartTimer();

        if (laserMovPosition != null) MoveLaser();
        
    }
    
    protected override void DeactivateTrap()
    {
        base.DeactivateTrap();
        laserObject.SetActive(false);
        StopAllCoroutines();
        StopMovementLaser();
        deactivateParticles.SetActive(true);
        laserSound.Stop();
        deactivateSound.Play();
        
        
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
        laserObject.SetActive(false);
        boxVisual.SetActive(false);
        StopAllCoroutines();
        
    }
    protected override void DisableTrap()
    {
        base.DisableTrap();
        laserObject.SetActive(false);
        StopMovementLaser();
        
    }

    private void MoveLaser()
    {
        if (canmove)
        {
            railVisual.SetActive(true);
            boxVisual.transform.DOLocalMove(laserMovPosition.localPosition, speedMovement).SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear).OnStepComplete(() =>
                {
                    onTrapCompletedMovement.Invoke();
                    // Debug.Log("Laser Trap Completed Movement");
                });
        }
    }
    private void StopMovementLaser()
    {
        if (laserMovPosition != null) 
            boxVisual.transform.DOKill();
        
        
    }
    private void StartTimer()
    {
        // Debug.Log("Laser Trap Started");
        StartCoroutine(InitTimer());

    }
    
   
    private void  EnableLaser( bool enable)
    {
        laserObject.SetActive(enable);
        if (enable)
        {
            laserCollision.ActivateLaser();
            laserSound.Play();
        }
        else
        {
            laserCollision.DeactivateLaser();
            laserSound.Stop();
        }
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

    public void GadgetInteraction(TypeOfGadget interactedGadget)
    {
        if(interactedGadget!= _gadgetInteractionType) return;

        DeactivateTrap();
    }
}
