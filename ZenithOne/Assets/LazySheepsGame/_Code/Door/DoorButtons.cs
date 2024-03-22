using UnityEngine;

public class DoorButtons : MonoBehaviour
{
    [SerializeField] private GenericDataChannelSO _doorCodeEvent;
    [SerializeField] private GameObject _door;
    [SerializeField] private string _codeNumber;
    
    public void OnButtonPressed()
    {
        _doorCodeEvent.RaiseStringEventGO(_codeNumber, _door);
    }
}
