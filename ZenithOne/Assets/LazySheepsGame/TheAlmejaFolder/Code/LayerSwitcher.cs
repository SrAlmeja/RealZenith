using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    public void TheSwitcher(GameObject selectedObject)
    {
        //Cambiar el DEFAULT_LAYER por el layer que contenga enemy o los que necesiten
        if (selectedObject.layer != StaticLayer.SELECTED_SHADER_LAYER)
        {
            selectedObject.layer = StaticLayer.SELECTED_SHADER_LAYER;
        }
        else
        {
            selectedObject.layer = StaticLayer.DEFAULT_LAYER;
        }
    }
}
