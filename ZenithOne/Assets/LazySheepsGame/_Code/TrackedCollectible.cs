using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class TrackedCollectible : Collectible
{
    public List<TrackedCollectible> ParentList { get; set; }
    private StudioEventEmitter _emitter;
    private bool _useEmitter;

    private void Start()
    {
        try
        {
            _emitter = GetComponent<StudioEventEmitter>();
        }
        catch
        {
            _useEmitter = false;
        }
    }

protected override void Collect()
    {
        if (_useEmitter)
        {
            ParentList.Remove(this);
            _emitter.Play();
            base.Collect();
        }
        else
        {
            ParentList.Remove(this);
            base.Collect();
        }
    }
    
}
