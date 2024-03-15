using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour, IGeneralTarget
{
    [SerializeField] private ScriptableEvent<float> playerReceivedDamageEvent;
    [SerializeField] private ScriptableEvent<float> UpdatePlayerHealthEvent;
    [SerializeField] private ScriptableEventNoParam playerDeathEvent;
    [SerializeField] private float _maxHealth = 100;

    private float _currentHealth;

    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            UpdatePlayerHealthEvent.Raise(_currentHealth);
        }
    } 
    public float MaxHealth => _maxHealth;
    
    private void Start()
    {
        _currentHealth = _maxHealth;
        playerReceivedDamageEvent.OnRaised += ReceiveDamageEvent;
    }

    
    private void OnEnable()
    {
        playerReceivedDamageEvent.OnRaised -= ReceiveDamageEvent;
    }

    public void ReceiveAggression(Vector3 direction, float dmg = 0)
    {
        SetDamage(dmg);
    }

    private void ReceiveDamageEvent(float dmg)
    {
        SetDamage(dmg);
    }

   
    private void SetDamage(float dmg)
    {
        _currentHealth -= dmg;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        playerDeathEvent.Raise();
    }
}
