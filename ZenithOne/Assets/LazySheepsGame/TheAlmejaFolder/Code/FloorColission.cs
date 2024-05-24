using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColission : MonoBehaviour
{
    private FloorSpawn _floorSpawn;
    [SerializeField] private bool isTheStart = false;
    [SerializeField] private GameObject spawnPoint;
    private GameObject _worldCenter;
    private float _distanceToCenter;
    private float distanceTolerance = 0.001f;
    private int roundedDistance;
    
    private void Awake()
    {
        _floorSpawn = GetComponentInParent<FloorSpawn>();
        if (_floorSpawn == null)
        {
            Debug.LogError("No se encontró el componente FloorSpawn en el padre de " + gameObject.name);
        }

        _worldCenter = GameObject.FindWithTag("Respawn");
        if (_worldCenter != null)
        {
            Debug.Log("Se encontró el centro");
        }
        else
        {
            Debug.LogError("El player no salio del refugio por que a esta escena no llego :v");
        }
    }

    private void FixedUpdate()
    {
        DistanceDetection();
        Debug.DrawLine(spawnPoint.transform.position, _worldCenter.transform.position);
    }

    private void DistanceDetection()
    {
        _distanceToCenter = Vector3.Distance(spawnPoint.transform.position, _worldCenter.transform.position);
        int roundedDistance = Mathf.RoundToInt(_distanceToCenter);
        Debug.Log("Distance " + roundedDistance);
        Spawn();
    }

    private void Spawn()
    {
        if (roundedDistance <= 1)
        {
            Debug.Log("Spanw");
            //_floorSpawn.RandomSelectionObject();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadZone"))
        {
            _floorSpawn.BackToThePool(this.gameObject);
        }
    }
}
