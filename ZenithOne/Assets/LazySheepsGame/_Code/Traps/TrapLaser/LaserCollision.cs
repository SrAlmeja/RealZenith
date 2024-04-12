using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float dmg = other.GetComponent<PlayerHealth>().MaxHealth;
            other.GetComponent<IGeneralTarget>().ReceiveAggression(Vector3.zero, dmg);
            Debug.Log("Player Hit by Laser".SetColor("#1AD3E2"));
        }
    }
    
    
}
