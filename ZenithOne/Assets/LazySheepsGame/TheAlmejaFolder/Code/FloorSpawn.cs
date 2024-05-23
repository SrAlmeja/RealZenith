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
    [SerializeField] private GameObject parentPrefab;

    private GameObject _selectedObject;
    private Vector3 _spawnPosition;
    private int _lastIndex;

    private void Awake()
    {
        _spawnPosition = spawner.transform.position;
    }
    
    
    
    public void RandomSelectionObject()
    {
        int newIndex;
        do
        {
            newIndex = UnityEngine.Random.Range(0, floorAssets.Length);
        } while (newIndex == _lastIndex);

        _selectedObject = floorAssets[newIndex];
        _lastIndex = newIndex;
        SpawnObject();
    }

    public void SpawnObject()
    {
        //Debug.Log("Spawneando " + _selectedObject.transform.name);
        LeanPool.Spawn(_selectedObject, _spawnPosition, Quaternion.identity, parentPrefab.transform);
    }
    
    

    public void BackToThePool(GameObject floor)
    {
        //Debug.Log("El objeto " + floor.transform.name + " fue regresado al pool");
        LeanPool.Despawn(floor);
    }
}
