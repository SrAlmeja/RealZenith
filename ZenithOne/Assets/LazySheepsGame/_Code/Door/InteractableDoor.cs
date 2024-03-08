using UnityEngine;

public class InteractableDoor : MonoBehaviour
{
    [SerializeField] private Rigidbody _doorRigidbody;
    
    private bool _unlockDoor = false;
    private Quaternion _initialRotation;
    
    
    void Start()
    {
        _initialRotation = transform.rotation;
    }
        
    void Update()
    {
        if (transform.rotation.eulerAngles.z != _initialRotation.eulerAngles.z)
        {
            _unlockDoor = true;
        }
        else
        {
            _unlockDoor = false;
        }
    }

    public void OpenDoor()
    {
        if (_unlockDoor)
        {
            _doorRigidbody.isKinematic = false;
        }
        else
        {
            _doorRigidbody.isKinematic = true;
        }
    }
}
