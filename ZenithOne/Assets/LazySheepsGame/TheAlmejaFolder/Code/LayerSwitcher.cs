using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    [SerializeField] public int _layerNumberToSwitch;
    [SerializeField] public GameObject _objectToTest;
    private int _originalLayer;


    [ContextMenu("Switch Layer")]
    private void Start()
    {
        _originalLayer = _objectToTest.layer;
    }

    public void GetMyLayer()
    {
        
    }
    
    public void TheSwitcher(int layerNumberToSwitch)
    {
        
        if (this.gameObject.layer != layerNumberToSwitch)
        {
            
            this.gameObject.layer = layerNumberToSwitch;
            
        }
        else
        {
            this.gameObject.layer = _originalLayer;
            
        }
    }
}
