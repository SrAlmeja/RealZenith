using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class SubtitlesTrigger : MonoBehaviour
{
    [SerializeField] ScriptableEventNoParam _onEnterTrigger;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _onEnterTrigger.Raise();
        }
    }
}
