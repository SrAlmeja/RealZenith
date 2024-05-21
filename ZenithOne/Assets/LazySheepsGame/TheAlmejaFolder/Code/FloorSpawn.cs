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

    private GameObject _selectedObject;
    private Vector3 _spawnPosition;
    private int _lastIndex;

    private void Awake()
    {
        _spawnPosition = spawner.transform.position;
        RandomSelectionObject();
        SpawnObject();
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
    }

    public void SpawnObject()
    {
        LeanPool.Spawn(_selectedObject, _spawnPosition, Quaternion.identity);
    }
    
    

    public void BackToThePool(GameObject floor)
    {
        LeanPool.Despawn(floor);
    }
}
