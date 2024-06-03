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
    [SerializeField] private GameObject parentPrefab;
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

    private void Update()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].position.z < zValue)
                tiles[i].position += (offset - Vector3.back * width * (tileCount));
        }
    }

    private void CreateTiles()
    {
        for (int i = 0; i < tileCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, floorAssets.Length);
            GameObject selectedObject = floorAssets[randomIndex];
            GameObject floor = LeanPool.Spawn(selectedObject,  offset - Vector3.back * width * i, quaternion.identity, parentPrefab.transform);
            tiles.Add(floor.transform);
        }
    }

}
