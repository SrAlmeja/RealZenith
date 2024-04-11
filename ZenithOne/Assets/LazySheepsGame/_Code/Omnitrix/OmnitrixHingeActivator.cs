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
        float value = _omnitrixTopFace.transform.localEulerAngles.z;
        if (value <= 2)
        {
            if (!_isOmnitrixActive)
            {
                _omnitrixActivationChannel.Raise();
                _isOmnitrixActive = true;
            }
        }
        else if (value >= 43)
        {
            if (_isOmnitrixActive)
            {
                _omnitrixActivationChannel.Raise();
                _isOmnitrixActive = false;
            }
        }

    }

    //====================================================================================================================//


    //Apagar menor a 43 y mayor a 312
    // Gadgets se activan en 270, 180 y 90

}
