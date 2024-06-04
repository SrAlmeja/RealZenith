using UnityEngine;
using Obvious.Soap;
using NaughtyAttributes;
using FMODUnity;

public class OmnitrixHingeActivator : MonoBehaviour
{
    [Header("Dependencies Channel SO")]
    [Required]
    [SerializeField] private ScriptableEventNoParam _omnitrixActivationChannel;
    [Required]
    [SerializeField] private ScriptableEventInt _omnitrixGadgetChannel;

    [Header("Dependencies")]
    [Required]
    [SerializeField] private GameObject _omnitrixTopFace;
    [Required]
    [SerializeField] private StudioEventEmitter _omnitrixSound;

    [Header("GadgetAngles")]
    [SerializeField] private float _gadgetFirstAngle = 45;
    [SerializeField] private float _gadgetSecondAngle = 90;
    [SerializeField] private float _gadgetThirdAngle = 135;

    [SerializeField] private int _angleDifference = 5;

    public float GadgetFirstAngle => _gadgetFirstAngle;
    public float GadgetSecondAngle => _gadgetSecondAngle;
    public float GadgetThirdAngle => _gadgetThirdAngle;
    public int AngleDifference => _angleDifference;


    private bool _isOmnitrixActive = false;

    private void Update()
    {
        UpdateOmnitrix();
    }

    private void UpdateOmnitrix()
    {
        float value = _omnitrixTopFace.transform.localEulerAngles.z;
        if (value <= _gadgetFirstAngle + _angleDifference && value >= _gadgetFirstAngle - _angleDifference)
        {
            EnableOmnitrix();
            _omnitrixGadgetChannel.Raise(0);
            _omnitrixSound.Play();
        }
        else if (value <= _gadgetSecondAngle + _angleDifference && value >= _gadgetSecondAngle - _angleDifference)
        {
            EnableOmnitrix();
            _omnitrixGadgetChannel.Raise(1);
            _omnitrixSound.Play();
        }
        else if (value <= _gadgetThirdAngle + _angleDifference && value >= _gadgetThirdAngle - _angleDifference)
        {
            EnableOmnitrix();
            _omnitrixGadgetChannel.Raise(2);
            _omnitrixSound.Play();
        }
        else if (value <= _angleDifference)
        {
            DisableOmnitrix();
            _omnitrixGadgetChannel.Raise(3);
            _omnitrixSound.Play();
        }

    }

    private void DisableOmnitrix()
    {
        if (_isOmnitrixActive)
        {
            _omnitrixActivationChannel.Raise();
            _isOmnitrixActive = false;
        }
    }

    private void EnableOmnitrix()
    {
        if (!_isOmnitrixActive)
        {
            _omnitrixActivationChannel.Raise();
            _isOmnitrixActive = true;
        }
    }

    //====================================================================================================================//


    //Apagar menor a 43 y mayor a 312
    // Gadgets se activan en 270, 180 y 90

}
