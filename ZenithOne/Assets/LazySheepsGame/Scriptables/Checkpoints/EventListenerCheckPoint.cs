using UnityEngine;
using UnityEngine.Events;

namespace Obvious.Soap
{
    [AddComponentMenu("Soap/EventListeners/EventListener"+nameof(CheckPoint))]
    public class EventListenerCheckPoint : EventListenerGeneric<CheckPoint>
    {
        [SerializeField] private EventResponse[] _eventResponses = null;
        protected override EventResponse<CheckPoint>[] EventResponses => _eventResponses;

        [System.Serializable]
        public class EventResponse : EventResponse<CheckPoint>
        {
            [SerializeField] private ScriptableEventCheckPoint _scriptableEvent = null;
            public override ScriptableEvent<CheckPoint> ScriptableEvent => _scriptableEvent;

            [SerializeField] private CheckPointUnityEvent _response = null;
            public override UnityEvent<CheckPoint> Response => _response;
        }

        [System.Serializable]
        public class CheckPointUnityEvent : UnityEvent<CheckPoint>
        {
            
        }
    }
}