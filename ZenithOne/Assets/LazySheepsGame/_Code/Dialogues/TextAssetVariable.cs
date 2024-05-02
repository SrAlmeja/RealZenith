using UnityEngine;

namespace Obvious.Soap
{
    [CreateAssetMenu(fileName = "scriptable_variable_" + nameof(TextAsset), menuName = "Soap/ScriptableVariables/"+ nameof(TextAsset))]
    public class TextAssetVariable : ScriptableVariable<TextAsset>
    {
        
    }
}
