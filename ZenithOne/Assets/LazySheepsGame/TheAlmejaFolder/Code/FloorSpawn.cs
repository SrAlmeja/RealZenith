using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using Unity.Mathematics;

public class FloorSpawn : MonoBehaviour
{
    [SerializeField] private float tileCount;
    [SerializeField] private GameObject[] floorAssets;
    [SerializeField] private float width;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float zValue;

    public List<Transform> tiles;
    
    private int _lastIndex;
    private GameObject _selectedObject;
    
    private void Start()
    {
        CreateTiles();
    }

    private void CreateTiles()
    {
        for (int i = 0; i < tileCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, floorAssets.Length);
            GameObject selectedObject = floorAssets[randomIndex];
            GameObject floor = LeanPool.Spawn(selectedObject,  offset - Vector3.back * width * i, quaternion.identity);
            tiles.Add(floor.transform);
        }
    }

    /*
    [Header("prefabs")]
   
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject parentPrefab;
    [SerializeField] private int width;




    
    private Vector3 _spawnPosition;


    private void Awake()
    {
        _spawnPosition = spawner.transform.position;
    }

    private void Start()
    {
        _spawnPosition = Vector3.forward * width * floorAssets.Length;
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
    }*/
}
