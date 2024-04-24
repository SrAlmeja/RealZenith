using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using UnityEditor;
using UnityEngine;

public class FallTrap : MonoBehaviour
{
    [SerializeField] private Vector3 scaleTrap = new Vector3(1, 1, 1);

    private void Start()
    {
        // ResizeTrap();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float dmg = other.GetComponent<PlayerHealth>().MaxHealth;
            other.GetComponent<IGeneralTarget>().ReceiveAggression(Vector3.zero, dmg);
            Debug.Log("Player Hit by Fall Trap".SetColor("#1AD3E2"));
        }
    }
    
    public void ResizeTrap()
    {
        transform.localScale = scaleTrap;
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(FallTrap))]
public class FallTrapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        FallTrap fallTrap = (FallTrap) target;
        if (GUILayout.Button("Resize Trap"))
        {
            fallTrap.ResizeTrap();
        }
    }
}


#endif

