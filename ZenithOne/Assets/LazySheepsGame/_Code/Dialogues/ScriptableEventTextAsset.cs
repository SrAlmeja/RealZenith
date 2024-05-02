using UnityEngine;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_event_" + nameof(TextAsset), menuName = "Soap/ScriptableEvents/"+ nameof(TextAsset))]
    public class ScriptableEventTextAsset : ScriptableEvent<TextAsset>
    {
        
    }
}
