using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    [SerializeField] private ScriptableEvent<string> laserCollisionEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            laserCollisionEvent.Raise("Player Hit by Laser");
        }
    }
    
    
}
