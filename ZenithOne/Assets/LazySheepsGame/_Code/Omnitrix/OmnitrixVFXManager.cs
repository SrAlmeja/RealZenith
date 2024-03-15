using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using Obvious.Soap;

public class OmnitrixVFXManager : MonoBehaviour
{
    [Header("Dependencies")]
    [Required][SerializeField] private ScriptableEventNoParam _omnitrixActivationChannel;
    [Required][SerializeField] private GameObject _platform;
    [SerializeField] private List<GameObject> _placePointsList = new List<GameObject>();

    [Header("Settings")]
    [SerializeField] private float _activateTime = 0.5f;
    [SerializeField] private float _deactivateTime = 0.5f;
    [SerializeField] private float _offsetTimeMultiplier = 0.2f;

    private List<Vector3> _initialPlacePointScaleList = new List<Vector3>();
    private Vector3 _initialPlatformScale;
    private bool _isOmnitrixActive = true;
    private bool _isDoneAnimating = true;

    private void OnEnable()
    {
        _omnitrixActivationChannel.OnRaised += DOActivate;
    }

    private void OnDisable()
    {
        _omnitrixActivationChannel.OnRaised -= DOActivate;
    }

    private void Start()
    {
        Prepare();
    }

    private void Prepare()
    {
        GetInitialScales();
        DOActivate();
    }

    private void DOActivate()
    {
        if (!_isDoneAnimating) return;
        _isDoneAnimating = false;
        _isOmnitrixActive = !_isOmnitrixActive;

        if (_isOmnitrixActive)
        {
            ActivatePlatform();
            Invoke("ActivateOmnitrix", _activateTime + (_placePointsList.Count * _offsetTimeMultiplier));
        } else
        {
            DeactivateOmnitrix();
            Invoke("DeactivatePlatform", _deactivateTime + (_placePointsList.Count * _offsetTimeMultiplier));
        }
    }

    private void ActivateOmnitrix()
    {
      for(int i = 0; i < _placePointsList.Count; i++)
        {
            _placePointsList[i].transform.localScale = Vector3.zero;
            _placePointsList[i].transform.DOScale(_initialPlacePointScaleList[i], _activateTime + (i * _offsetTimeMultiplier));
        }
      _isDoneAnimating = true;
    }

    private void DeactivateOmnitrix()
    {
        for (int i = _placePointsList.Count - 1; i >= 0; i--)
        {
            _placePointsList[i].transform.localScale = _initialPlacePointScaleList[i];
            _placePointsList[i].gameObject.transform.DOScale(Vector3.zero, _activateTime + ((_placePointsList.Count - i) * _offsetTimeMultiplier));
        }
    }

    private void ActivatePlatform()
    {
        ActivateGameObjects(true);
        _platform.transform.localScale = Vector3.zero;
        _platform.transform.DOScale(_initialPlatformScale, _activateTime);
    }

    private void DeactivatePlatform()
    {
        _platform.transform.localScale = _initialPlatformScale;
        _platform.transform.DOScale(0, _deactivateTime).OnComplete(()=> ActivateGameObjects(false));
        _isDoneAnimating = true;
    }

    private void ActivateGameObjects(bool value)
    {
        foreach(GameObject placePoint in _placePointsList)
        {
            placePoint.SetActive(value);
        }

        _platform.SetActive(value);
    }

    private void GetInitialScales()
    {
        _initialPlacePointScaleList.Clear();
        foreach(GameObject placePoint in _placePointsList)
        {
            _initialPlacePointScaleList.Add(placePoint.transform.localScale);
        }
        _initialPlatformScale = _platform.transform.localScale;
    }
}