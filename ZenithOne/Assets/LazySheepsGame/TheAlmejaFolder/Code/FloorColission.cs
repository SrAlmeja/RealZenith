using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColission : MonoBehaviour
{
    private FloorSpawn _floorSpawn;

    private void Awake()
    {
        GetComponentInParent<FloorColission>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadZone"))
        {
            //_floorSpawn.BackToThePool(gameObject);
            Debug.Log("Destruyendo piso");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            //_floorSpawn.RandomSelectionObject();
            //_floorSpawn.SpawnObject();
            Debug.Log("Spawneando piso");
        }
    }
}
