using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private ScriptableEventCheckPoint _checkPointEvent;
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
            if(_playerHasReached) return;
            _playerHasReached = true;
            _checkPointEvent.Raise(this);
            Debug.Log("Player has reached checkpoint".SetColor("#FED744"));
            
        }
    }
    
    
}
