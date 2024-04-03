using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
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


    [SerializeField] PlayerVignette _playerVignette;
    private Vector3 _initialPosition;


    #region Unity methods



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

    #endregion

    #region public methods

    public void MovePlayerToCheckPoint(Vector3 checkPointPosition)
    {
        _initialPosition = checkPointPosition;
        _playerVignette.OnTransitionEnd += EnablePlayer;
        
    }

    private void EnablePlayer()
    {
        transform.position = _initialPosition;
        _playerVignette.OnTransitionEnd -= EnablePlayer;
        _playerVignette.DoFadeOut();
        Debug.Log("Player moved to last checkpoint = ".SetColor("#FED744") + _initialPosition);
    }

    #endregion
}