using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public static PlayerSpawn Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerSpawn>();
            }

            return _instance;
        }
        private set => _instance = value;
    }
    private static PlayerSpawn _instance;
    
    private Vector3 _initialPosition;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void MovePlayerToCheckPoint(Vector3 checkPointPosition)
    {
        transform.position = checkPointPosition;
    }
    
    
    
}
