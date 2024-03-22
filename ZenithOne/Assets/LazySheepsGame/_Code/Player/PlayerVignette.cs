using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class PlayerVignette : MonoBehaviour
{
    #region SerializedFields
    
    [SerializeField] private ScriptableEventNoParam onTransitionEvent;

    
    [SerializeField] private OVRVignette _vignette;
    [SerializeField] private float _transitionDuration = 1f;
    [SerializeField] private float _vignetteMax = 100f;
    [SerializeField] private float _vignetteMin = -10f;
    #endregion

    #region private variables
    
    private float _valueVignette = 0f;
    private float _time = 0f;
    
    private TransitionState _transitionState = TransitionState.None;
    
    private enum TransitionState
    {
        None,
        FadingIn,
        FadingOut
    }
    
    #endregion

    void Start()
    {
        SetVignetteValue(_vignetteMin);
        StartCoroutine(TransitionCoroutine());
        
        
        onTransitionEvent.OnRaised += DoFadeIn;
    }

    void Update()
    {
        if (_transitionState != TransitionState.None)
        {
            _time += Time.deltaTime;
            PerformTransition();
        }
    }
    
    public void DoFadeIn()
    {
        _transitionState = TransitionState.FadingIn;
    }
    public void DoFadeOut()
    {
        _transitionState = TransitionState.FadingOut;
    }

    #region private methods
    
    private void SetVignetteValue(float value)
    {
        _vignette.VignetteFieldOfView = value;
    }
    
    private void PerformTransition()
    {
        if (_transitionState == TransitionState.FadingOut)
        {
            _valueVignette = Mathf.Lerp(_vignetteMin, _vignetteMax, _time / _transitionDuration);
        }
        else
        {
            _valueVignette = Mathf.Lerp(_vignetteMax, _vignetteMin, _time / _transitionDuration);
        }

        SetVignetteValue(_valueVignette);
        CheckIfTransitionEnded();
    }
    
    private void CheckIfTransitionEnded()
    {
        if (_time >= _transitionDuration)
        {
            _transitionState = TransitionState.None;
            _time = 0f;
        }
    }
    
    private IEnumerator TransitionCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        DoFadeOut();
    }
    #endregion

    
    
    
}