using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    [SerializeField] private float iSpeed = 0;
    [SerializeField] private float fSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float aceleration;
    [SerializeField] private Vector3 direction;

    private void Awake()
    {
        speed = iSpeed;
    }

    void FixedUpdate()
    {
        Aceleration();
        Move();
    }

    private void Aceleration()
    {
        if (speed <= fSpeed)
        {
            speed += aceleration * Time.deltaTime;
            if (speed > fSpeed)
            {
                speed = fSpeed;
            }
        }
    }

    private void Move()
    {
        transform.Translate(direction * speed *Time.fixedDeltaTime);   
    }
    
}
