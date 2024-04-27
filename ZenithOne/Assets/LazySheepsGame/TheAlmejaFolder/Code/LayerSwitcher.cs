using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    [SerializeField] public int _layerNumberToSwitch;
    [SerializeField] public GameObject _objectToTest;
    
    
    
    [ContextMenu("Switch Layer")]
    public void TheSwitcher(GameObject testBudy, int layerNumberToSwitch)
    {
        int originalLayer = testBudy.layer;
        if (testBudy.layer != layerNumberToSwitch)
        {
            
            testBudy.layer = layerNumberToSwitch;    
        }
        else
        {
            testBudy.layer = originalLayer;
        }
    }
}
