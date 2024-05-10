using Obvious.Soap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NewOmnitrixTestRotations : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ScriptableEventBool _grabbingOmnitrixChannel;

    [Header("Settings")]
    [SerializeField] private float _rotationMultiplier = 1.0f;

    private InputDevice _rightController;
    private bool _grabbingOmnitrix = false;

    private Quaternion _initialObjectRotation;
    private Quaternion _initialControllerRotation;

    private void OnEnable()
    {
        _grabbingOmnitrixChannel.OnRaised += UpdateGrabbing;
    }

    private void OnDisable()
    {
        _grabbingOmnitrixChannel.OnRaised -= UpdateGrabbing;
    }

    void Update()
    {
        if (!_rightController.isValid) InitializeInputDevices();

        RotateObject();
    }

    private void InitializeInputDevices()
    {

        if (!_rightController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _rightController);
    }

    private void InitializeInputDevice(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();
        //Call InputDevices to see if it can find any devices with the characteristics we're looking for
        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics, devices);

        //Our hands might not be active and so they will not be generated from the search.
        //We check if any devices are found here to avoid errors.
        if (devices.Count > 0)
        {
            inputDevice = devices[0];
        }
    }

    private void UpdateGrabbing(bool value)
    {
        if (value == _grabbingOmnitrix) return;
        _grabbingOmnitrix = value;
        if(_grabbingOmnitrix) UpdateRotations();
        else ClampRotation();
    }

    private void UpdateRotations()
    {
        _initialObjectRotation = transform.localRotation;
        if(_rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightRotation))
        {
            _initialControllerRotation = rightRotation;
        }
    }

    private void RotateObject()
    {
        if (!_grabbingOmnitrix) return;

        _rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightRotation);
        Quaternion deltaQuaternion = Quaternion.Inverse(_initialControllerRotation) * rightRotation;

        Vector3 eulerDelta = deltaQuaternion.eulerAngles;
        //Debug.Log(eulerDelta);
        float rotationZ = eulerDelta.x * _rotationMultiplier;

        Quaternion newRotation = Quaternion.Euler(_initialObjectRotation.eulerAngles.x, _initialObjectRotation.eulerAngles.y, _initialObjectRotation.eulerAngles.z + rotationZ);

        transform.localRotation = newRotation;
    }

    private void ClampRotation()
    {
        if (transform.localEulerAngles.z > 135)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 135);
        } else if (transform.localEulerAngles.z < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}

