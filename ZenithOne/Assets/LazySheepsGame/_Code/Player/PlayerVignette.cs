using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVignette : MonoBehaviour
{
    [SerializeField] private OVRVignette _vignette;
    [SerializeField] private float _transitionDuration = 1f;
    [SerializeField] private float _vignetteMax = 100f;
    [SerializeField] private float _vignetteMin = -10f;
    
    private bool _isTransitioning = false;
    private float _valueVignette = 0f;
    private float _time = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DoFadeIn();
        }
        
        if (_isTransitioning)
        {
            _time += Time.deltaTime;
            _valueVignette = Mathf.Lerp(_vignetteMax, _vignetteMin, _time / _transitionDuration);
            SetVignetteValue(_valueVignette);
            
            if (_time >= _transitionDuration)
            {
                _isTransitioning = false;
                _time = 0f;
            }
        }
    }
    
    public void DoFadeIn()
    {
        _isTransitioning = true;
    }
    
    private void SetVignetteValue(float value)
    {
        _vignette.VignetteFieldOfView = value;
    }
    
    
}
