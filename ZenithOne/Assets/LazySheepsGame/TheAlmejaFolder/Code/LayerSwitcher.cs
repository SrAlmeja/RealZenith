using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    [SerializeField] public GameObject _objectToTest;


    [ContextMenu("Switch Layer")]
    
    public void TheSwitcher()
    {
        //Cambiar el DEFAULT_LAYER por el layer que contenga enemy o los que necesiten
        if (_objectToTest.layer != StaticLayer.SELECTED_SHADER_LAYER)
        {
            _objectToTest.layer = StaticLayer.SELECTED_SHADER_LAYER;
        }
        else
        {
            _objectToTest.layer = StaticLayer.DEFAULT_LAYER;
        }
    }
}
