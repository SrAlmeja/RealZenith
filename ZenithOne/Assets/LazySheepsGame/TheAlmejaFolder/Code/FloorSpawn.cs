using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class FloorSpawn : MonoBehaviour
{
    [Header("prefabs")]
    [SerializeField] private GameObject[] floorAssets;
    [SerializeField] private GameObject spawner;
    
    private Vector3 _spawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(""))
        {
            
        }
    }
}
