using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColission : MonoBehaviour
{
    private FloorSpawn _floorSpawn;
    [SerializeField] private bool isTheStart = false;
    [SerializeField] private GameObject spawnPoint;
    private GameObject _player;
    private Vector3 _playerPosition;
    private float _playerDistance;
    [SerializeField] private float rangeDistance;
    
    private void Awake()
    {
        _floorSpawn = GetComponentInParent<FloorSpawn>();
        if (_floorSpawn == null)
        {
            Debug.LogError("No se encontró el componente FloorSpawn en el padre de " + gameObject.name);
        }

        _player = GameObject.FindWithTag("Player");
        if (_player != null)
        {
            Debug.Log("Se encontró al player");
        }
        else
        {
            Debug.LogError("El player no salio del refugio por que a esta escena no llego :v");
        }
    }

    private void FixedUpdate()
    {
        DistanceDetection();
    }

    private void DistanceDetection()
    {
            _playerDistance = Vector3.Distance(_player.transform.position, spawnPoint.transform.position);
            Debug.Log("Distancia entre spawn point y jugador: " + _playerDistance);
            Spawn();
    }

    private void Spawn()
    {
        if (!isTheStart)
        {
            if (_playerDistance == rangeDistance)
            {
                Debug.Log("Llego al punto");
                //_floorSpawn.RandomSelectionObject();
            }    
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
