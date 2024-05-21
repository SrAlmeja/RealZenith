using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColission : MonoBehaviour
{
    private FloorSpawn _floorSpawn;

    private void Awake()
    {
        _floorSpawn = GetComponentInParent<FloorSpawn>();
        if (_floorSpawn == null)
        {
            Debug.LogError("No se encontró el componente FloorSpawn en el padre de " + gameObject.name);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadZone"))
        {
            _floorSpawn.BackToThePool(this.gameObject);
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
