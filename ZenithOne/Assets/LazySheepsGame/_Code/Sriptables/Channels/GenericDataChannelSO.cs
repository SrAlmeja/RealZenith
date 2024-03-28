using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/Events/Generic Data Event Channel")]
public class GenericDataChannelSO : ScriptableObject
{
    public UnityAction VoidEvent;
    public UnityAction<int> IntEvent;
    public UnityAction<string, GameObject> StringEventGO;
    public UnityAction<float> FloatEvent;
    public UnityAction<double> DoubleEvent;
    public UnityAction<string> StringEvent;
    public UnityAction<bool> BoolEvent;

    public void RaiseVoidEvent()
    {
        VoidEvent?.Invoke();
    }

    public void RaiseIntEvent(int value)
    {
        IntEvent?.Invoke(value);
    }
    
    public void RaiseStringEventGO(string value, GameObject gameObject)
    {
        StringEventGO?.Invoke(value, gameObject);
    }

    public void RasieFloatEvent(float value)
    {
        FloatEvent?.Invoke(value);
    }

    public void RaiseDoubleEvent(double value)
    {
        DoubleEvent?.Invoke(value);
    }

    public void RaiseStringEvent(string value)
    {
        StringEvent?.Invoke(value);
    }

    public void RaiseBoolEvent(bool value)
    {
        BoolEvent?.Invoke(value);
    }
}
