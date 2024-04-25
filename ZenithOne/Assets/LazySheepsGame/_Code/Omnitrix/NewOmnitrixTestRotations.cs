using Obvious.Soap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NewOmnitrixTestRotations : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ScriptableEventBool _grabbingOmnitrixChannel;

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
    }

    private void UpdateRotations()
    {
        _initialObjectRotation = transform.localRotation;
        if(_rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightRotation))
        {
            _initialControllerRotation = rightRotation;
        }
        //Debug.Log("Initial Controller Rotation: " + _initialControllerRotation);
        Debug.Log("Initial Controller Rotation: " + _initialControllerRotation.eulerAngles);
        //Debug.Log("Initial Object Rotation: " + _initialObjectRotation);
    }

    private void RotateObject()
    {
        if(_grabbingOmnitrix == false) return;

        _rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightRotation);
        Quaternion deltaQuaternion = Quaternion.Inverse(_initialControllerRotation) * rightRotation;



        Vector3 eulerDelta = deltaQuaternion.eulerAngles;
        float rotationZ = eulerDelta.x;

        //Debug.Log(eulerDelta);
       // Debug.Log("Euler X "+ eulerDelta.x);

        // Apply the change in rotation to the object's local rotation
        /*
        if(transform.localRotation.z < 0)
        {
            //transform.localRotation.z = 0;
            return;
        }

        if(transform.localRotation.z > 135)
        {
            return;
        }
        */

        gameObject.transform.localRotation = Quaternion.Euler(_initialObjectRotation.x, _initialObjectRotation.y, _initialObjectRotation.z + rotationZ);
    }
}

