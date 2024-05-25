using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    public void OnSelected(GameObject selectedObject)
    {
        selectedObject.layer = StaticLayer.SELECTED_SHADER_LAYER;
    }

    public void OnDeselected(GameObject selectedObject)
    {
        selectedObject.layer = StaticLayer.DEFAULT_LAYER;
    }
}
