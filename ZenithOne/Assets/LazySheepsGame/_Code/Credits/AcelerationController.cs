using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcelerationController : MonoBehaviour
{
    [Header("Aceleration")]
    [SerializeField] private float iSpeed = 0;
    [SerializeField] private float fSpeed;
    [SerializeField] private float aceleration;
    [SerializeField] private SOFloat Speed;

    
    void Start()
    {
        Speed.value = iSpeed;
    }

    private void FixedUpdate()
    {
        Aceleration();
    }
    
    private void Aceleration()
    {
        if (Speed.value <= fSpeed)
        {
            Speed.value += aceleration * Time.deltaTime;
            if (Speed.value > fSpeed)
            {
                Speed.value = fSpeed;
            }
        }
    }
}
