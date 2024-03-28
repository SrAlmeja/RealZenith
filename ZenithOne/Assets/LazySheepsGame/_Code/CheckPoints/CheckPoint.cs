using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool _playerHasReached;
    public bool PlayerHasReached => _playerHasReached;
    public Vector3 CheckPointPosition
    {
        get => transform.position;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            _playerHasReached = true;
        }
    }
    
    
}
