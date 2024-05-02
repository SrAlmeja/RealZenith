using UnityEngine;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_event_" + nameof(DialogueBase), menuName = "Soap/ScriptableEvents/"+ nameof(DialogueBase))]
    public class ScriptableEventDialogueBase : ScriptableEvent<DialogueBase>
    {
        
    }
}
