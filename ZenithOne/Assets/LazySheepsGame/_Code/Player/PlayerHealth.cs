using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IGeneralTarget
{
    [SerializeField] private ScriptableEvent<float> playerReceivedDamageEvent;
    [SerializeField] private ScriptableEvent<float> playerHealthEvent;
    [SerializeField] private ScriptableEventNoParam playerDeathEvent;
    [SerializeField] private float _maxHealth = 100;

    private float _currentHealth;
    
    private void Start()
    {
        _currentHealth = _maxHealth;
        // playerReceivedDamageEvent.OnRaised += ReceiveAggression;
    }

    // private void ReceiveAggression(Vector3 dir, float damage)
    // {
    //     Debug.Log("Player received damage: " + damage);
    //     ReceiveAggression(Vector3.zero, damage);
    // }

    public void ReceiveAggression(Vector3 direction, float dmg = 0)
    {
        _currentHealth -= dmg;
        // playerHealthEvent.Raise(_currentHealth);
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
