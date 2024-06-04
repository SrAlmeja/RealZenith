using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    public void OnSelected(GameObject selectedObject = null, GameObject[] objects = null)
    {
        if (selectedObject != null)
        {
            selectedObject.layer = StaticLayer.SELECTED_SHADER_LAYER;
        }
        else
        {
            foreach (var obj in objects)
            {
                obj.layer = StaticLayer.SELECTED_SHADER_LAYER;
            }
        }
    }
    

    public void OnDeselected(GameObject selectedObject = null, GameObject[] objects = null)
    {
        if (selectedObject != null)
        {
            selectedObject.layer = StaticLayer.ENEMY_LAYER;
        }
        else
        {
            
            foreach (var obj in objects)
            {
                obj.layer = StaticLayer.ENEMY_LAYER;
            }
        }
    }
    
    public void DeselectObjectsDefault(GameObject[] objects = null)
    {
        foreach (var obj in objects)
        {
            obj.layer = StaticLayer.DEFAULT_LAYER;
        }
    }
   
    


    public GameObject[] Objects;

}
