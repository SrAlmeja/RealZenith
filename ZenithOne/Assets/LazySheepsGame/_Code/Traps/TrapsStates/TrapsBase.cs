using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LazyGames;
public class TrapsBase : StateManager<TrapsStates,TrapsBase>
{
    [SerializeField] private TrapsStates initialState;
    private void Awake()
    {
        PrepareSates();
        CallCurrentState();
    }

    private void PrepareSates()
    {
        States.Add(TrapsStates.Inactive, new TrapInnactiveState(TrapsStates.Inactive, this));
        States.Add(TrapsStates.Active, new TrapActiveState(TrapsStates.Active, this));
        States.Add(TrapsStates.Triggered, new TrapTriggeredState(TrapsStates.Triggered, this));
        States.Add(TrapsStates.Disabled, new TrapDisabledState(TrapsStates.Disabled, this));
        States.Add(TrapsStates.Destroyed, new TrapDestroyedState(TrapsStates.Destroyed, this));
        
        CurrentState = States[initialState];
        Debug.Log("Trap State = ".SetColor("#FED744") + CurrentState);
    }

    private void CallCurrentState()
    {
        switch (CurrentState.StateKey)
        {
            case TrapsStates.Inactive:
                DeactivateTrap();
                break;
            case TrapsStates.Active:
                ActivateTrap();
                break;
            case TrapsStates.Triggered:
                TriggerTrap();
                break;
            case TrapsStates.Disabled:
                DisableTrap();
                break;
            
        }
    }


    protected virtual void ActivateTrap()
    {
        TransitionToState(TrapsStates.Active);
        Debug.Log("Trap Activated! = ".SetColor("#FED744") + gameObject.name);
    }
    
    protected virtual void DeactivateTrap()
    {
        TransitionToState(TrapsStates.Inactive);
        Debug.Log("Trap Deactivated! = ".SetColor("#FED744") + gameObject.name);
    }
    
    protected virtual void ResetTrap()
    {
        TransitionToState(TrapsStates.Inactive);
        Debug.Log("Trap Reset! = ".SetColor("#FED744") + gameObject.name);
    }
    
    protected virtual void TriggerTrap()
    {
        TransitionToState(TrapsStates.Triggered);
        Debug.Log("Trap Triggered! = ".SetColor("#FED744") + gameObject.name);
    }
    
    protected virtual void DestroyTrap()
    {
        TransitionToState(TrapsStates.Destroyed);
        Debug.Log("Trap Destroyed! = ".SetColor("#FED744") + gameObject.name);
    }
    
    protected virtual void DisableTrap()
    {
        TransitionToState(TrapsStates.Disabled);
        Debug.Log("Trap Disabled! = ".SetColor("#FED744") + gameObject.name);
    }
}



