using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : DialogueBase
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Interact with trigguer Dialogue");
            SendDialogue();
        }
    }
}
