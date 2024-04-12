using UnityEngine;
using Obvious.Soap;

public class OmnitrixHingeActivator : MonoBehaviour
{
    [Header("Dependencies Channel SO")]
    [SerializeField] private ScriptableEventNoParam _omnitrixActivationChannel;
    [SerializeField] private ScriptableEventInt _omnitrixGadgetChannel;

    [Header("Dependencies")]
    [SerializeField] private GameObject _omnitrixTopFace;

    private bool _isOmnitrixActive = false;

    private void Update()
    {
        UpdateOmnitrix();
    }

    private void UpdateOmnitrix()
    {
        float value = _omnitrixTopFace.transform.localEulerAngles.z;
        if (value <= 272 && value >= 268)
        {
            EnableOmnitrix();
            _omnitrixGadgetChannel.Raise(0);
        }
        else if (value <= 227 && value >= 223)
        {
            EnableOmnitrix();
            _omnitrixGadgetChannel.Raise(1);
        }
        else if (value <= 182 && value >= 178)
        {
            EnableOmnitrix();
            _omnitrixGadgetChannel.Raise(2);
        }else if (value >= 312)
        {
            DisableOmnitrix();
            _omnitrixGadgetChannel.Raise(3);
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
