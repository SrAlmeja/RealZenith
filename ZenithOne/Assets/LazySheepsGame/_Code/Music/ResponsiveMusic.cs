using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Obvious.Soap;
using com.LazyGames.Dz.Ai;
using NaughtyAttributes;

public class ResponsiveMusic : MonoBehaviour
{
    [Header("Dependencies")]
    [Required]
    [SerializeField] private Transform _playerTransform;

    [Header("FMOD Dependencies")]
    [Required]
    [SerializeField] private StudioEventEmitter _alertSound;
    [Required]
    [SerializeField] private StudioEventEmitter _alertMusic;
    [Required]
    [SerializeField] private StudioEventEmitter _stealthMusic;
    [Required]
    [SerializeField] private StudioEventEmitter _finishSound;

    [Header("SO Channels Dependencies")]
    [Required]
    [SerializeField] private ScriptableEvent<EnemyStateWrapper> _enemyStateWrapperSo;

    [Header("Settings")]
    [CurveRange(3f, 1f, 10f, 0f)]
    [SerializeField] private AnimationCurve _minMaxEnemyDistanceParameter;

    private List<EnemyStateWrapper> _patrolingEnemiesList = new List<EnemyStateWrapper>();
    private List<EnemyStateWrapper> _searchingEnemiesList = new List<EnemyStateWrapper>();
    private List<EnemyStateWrapper> _chasingEnemiesList = new List<EnemyStateWrapper>();

    private MusicState _currentMusicState = MusicState.Stealth;

    private void OnEnable()
    {
        _enemyStateWrapperSo.OnRaised += ReceiveEnemy;
    }

    private void OnDisable()
    {
        _enemyStateWrapperSo.OnRaised -= ReceiveEnemy;
    }

    private void Update()
    {
        ChaseParam();
    }


    private void ReceiveEnemy(EnemyStateWrapper enemy)
    {
        Debug.Log("Received enemy " + enemy.EnemyBt.gameObject.name);
        RemoveFromList(enemy);
        SortIntoList(enemy);
        TransitionMusic();
    }

    private void RemoveFromList(EnemyStateWrapper enemy)
    {
        if(_patrolingEnemiesList.Contains(enemy)) _patrolingEnemiesList.Remove(enemy);
        else if (_searchingEnemiesList.Contains(enemy)) _searchingEnemiesList.Remove(enemy);
        else if (_chasingEnemiesList.Contains(enemy)) _chasingEnemiesList.Remove(enemy);
    }
    
    private void SortIntoList(EnemyStateWrapper enemy)
    {
        switch (enemy.State)
        {
            case EnemyState.Patrolling:
                _patrolingEnemiesList.Add(enemy);
                Debug.Log("Added to patroling list");
                break;
            case EnemyState.Searching:
                _searchingEnemiesList.Add(enemy);
                Debug.Log("Added to searching list");
                break;
            case EnemyState.Chasing:
                _chasingEnemiesList.Add(enemy);
                Debug.Log("Added to chasing list");
                break;
        }
    }

    private MusicState CheckForNewState()
    {
        if(_chasingEnemiesList.Count > 0)
        {     
            return MusicState.Chasing;
        }

        if(_searchingEnemiesList.Count > 0)
        {
            return MusicState.Alert;
        }

        return MusicState.Stealth;
    }

    private void TransitionMusic()
    {
        if(_currentMusicState == CheckForNewState()) return;

        switch (CheckForNewState())
        {
            case MusicState.Stealth:
                _alertMusic.Stop();
                _finishSound.Play();
                _stealthMusic.Play();
                _currentMusicState = MusicState.Stealth;
                break;

            case MusicState.Alert:
                if(_currentMusicState == MusicState.Stealth)
                {
                    _stealthMusic.Stop();
                    _finishSound.Play();
                    _alertMusic.Play();
                    _alertMusic.Params[0].Value = 0;
                }

                if(_currentMusicState == MusicState.Chasing)
                {
                    _alertMusic.Params[0].Value = 0;
                }
                _currentMusicState = MusicState.Alert;
                break;

            case MusicState.Chasing:      
                if(_currentMusicState == MusicState.Stealth)
                {
                    _stealthMusic.Stop();
                    _alertSound.Play();
                    _alertMusic.Play();
                    _alertMusic.Params[0].Value = GetChaseParamValue(GetClosestEnemy());
                }

                if(_currentMusicState == MusicState.Alert)
                {
                    _alertMusic.Params[0].Value = GetChaseParamValue(GetClosestEnemy());
                }
                _currentMusicState = MusicState.Chasing;
                break;
        }
    }

    private GameObject GetClosestEnemy()
    {
        if(_chasingEnemiesList.Count == 0) return null ;

        GameObject closestEnemy = _chasingEnemiesList[0].EnemyBt.gameObject;

        foreach (EnemyStateWrapper enemy in _chasingEnemiesList)
        {
            if(Vector3.Distance(_playerTransform.position, enemy.EnemyBt.gameObject.transform.position) < Vector3.Distance(_playerTransform.position, closestEnemy.transform.position))
            {
                closestEnemy = enemy.EnemyBt.gameObject;
            }
        }

        return closestEnemy;
    }

    private float GetChaseParamValue(GameObject enemy)
    {
        if(enemy == null) return 0;
        Debug.Log("Distance to closest enemy: " + _minMaxEnemyDistanceParameter.Evaluate(Vector3.Distance(_playerTransform.position, enemy.transform.position)));
        return _minMaxEnemyDistanceParameter.Evaluate(Vector3.Distance(_playerTransform.position, enemy.transform.position));
    }

    private void ChaseParam()
    {
        if (_chasingEnemiesList.Count <= 0) return;

        _alertMusic.Params[0].Value = GetChaseParamValue(GetClosestEnemy());
    }


    public enum MusicState
    {
        Stealth,
        Alert,
        Chasing,
    }
}
