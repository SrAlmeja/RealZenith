using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using Obvious.Soap;
using FMODUnity;

public class OmnitrixVFXManager : MonoBehaviour
{
    [Header("Dependencies")]
    [Required][SerializeField] private ScriptableEventNoParam _omnitrixActivationChannel;
    [Required][SerializeField] private ScriptableEventInt _omnitrixGadgetChannel;
    [Required][SerializeField] private GameObject _particleSystem;
    [SerializeField] private List<GameObject> _placePointsList = new List<GameObject>();
    [Required]
    [SerializeField] private StudioEventEmitter _omnitrixSound;

    [Header("Settings")]
    [SerializeField] private float _activateTime = 0.5f;
    [SerializeField] private float _deactivateTime = 0.5f;
    [SerializeField] private float _offsetTimeMultiplier = 0.2f;

    private List<Vector3> _initialPlacePointScaleList = new List<Vector3>();
    private Vector3 _initialPlatformScale;
    private bool _isOmnitrixActive = true;
    private bool _isDoneAnimating = true;
    private int _activeGadget = 3;

    private void OnEnable()
    {
        _omnitrixActivationChannel.OnRaised += DOActivate;
        _omnitrixGadgetChannel.OnRaised += ActivateSingleGadget;
    }

    private void OnDisable()
    {
        _omnitrixActivationChannel.OnRaised -= DOActivate;
        _omnitrixGadgetChannel.OnRaised -= ActivateSingleGadget;
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
            ActivateOmnitrix();
            //Invoke("ActivateOmnitrix", _activateTime + (_placePointsList.Count * _offsetTimeMultiplier));
        } else
        {
            DeactivateOmnitrix();
            Invoke("DeactivatePlatform", _deactivateTime);
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
        _particleSystem.transform.localScale = Vector3.zero;
        _particleSystem.transform.DOScale(_initialPlatformScale, _activateTime);
    }

    private void DeactivatePlatform()
    {
        _particleSystem.transform.localScale = _initialPlatformScale;
        _particleSystem.transform.DOScale(0, _deactivateTime).OnComplete(()=> ActivateGameObjects(false));
        _isDoneAnimating = true;
    }

    private void ActivateGameObjects(bool value)
    {
        foreach(GameObject placePoint in _placePointsList)
        {
            placePoint.SetActive(value);
        }

        _particleSystem.SetActive(value);
    }

    private void GetInitialScales()
    {
        _initialPlacePointScaleList.Clear();
        foreach(GameObject placePoint in _placePointsList)
        {
            _initialPlacePointScaleList.Add(placePoint.transform.localScale);
        }
        _initialPlatformScale = _particleSystem.transform.localScale;
    }

    private void ActivateSingleGadget(int gadgetToActivate)
    {
        if(_activeGadget == gadgetToActivate) return;
        _omnitrixSound.Play();
        _activeGadget = gadgetToActivate;
        // Debug.Log(gadgetToActivate);
        for (int i = _placePointsList.Count - 1; i >= 0; i--)
        {
            _placePointsList[i].transform.localScale = Vector3.zero;
            _placePointsList[i].SetActive(false);
        }
        if (gadgetToActivate == 3) return;
        _placePointsList[gadgetToActivate].SetActive(true);
        _placePointsList[gadgetToActivate].transform.DOScale(_initialPlacePointScaleList[gadgetToActivate], _activateTime + (gadgetToActivate * _offsetTimeMultiplier));
        _placePointsList[gadgetToActivate].transform.DOScale(_initialPlacePointScaleList[gadgetToActivate], _activateTime + _offsetTimeMultiplier);
    }
}
