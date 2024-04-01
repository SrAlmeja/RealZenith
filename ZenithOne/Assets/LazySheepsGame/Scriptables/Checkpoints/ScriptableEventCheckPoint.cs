using UnityEngine;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_event_" + nameof(CheckPoint), menuName = "Soap/ScriptableEvents/"+ nameof(CheckPoint))]
    public class ScriptableEventCheckPoint : ScriptableEvent<CheckPoint>
    {
        
    }
}
