using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class FloorMovement : MonoBehaviour
{
    [SerializeField] private SOFloat speed;
    [SerializeField] private Vector3 direction;

    private AcelerationController _acelerationController;

    void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        transform.Translate(direction * speed.value * Time.fixedDeltaTime);   
    }
    
}
