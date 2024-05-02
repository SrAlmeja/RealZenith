using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Obvious.Soap;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserCollision : MonoBehaviour
{
    [SerializeField] private Transform laserStart;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserDistance = 10f;
    [SerializeField] private LayerMask layerMask;
    
    private Ray _ray;
    private bool _cast;
    private bool _damageApplied = false;

    public void ActivateLaser()
    {
        lineRenderer.enabled = true; 
    }

    public void DeactivateLaser()
    {
        lineRenderer.enabled = false;
        lineRenderer.SetPosition(0, laserStart.position);
        lineRenderer.SetPosition(1, laserStart.position);
        
    }



    private void Update()
    {
        if (lineRenderer.enabled)
        {
            DrawLaser();
        }
    }

    private void DrawLaser()
    {
        _ray = new Ray(laserStart.position, laserStart.forward);
        _cast = Physics.Raycast(_ray, out var hit, laserDistance, layerMask);
        Vector3 hitPosition = _cast ? hit.point : laserStart.position + laserStart.forward * laserDistance;
        lineRenderer.SetPosition(0, laserStart.position);
        lineRenderer.SetPosition(1, hitPosition);
        
        if(_cast && hit.collider.CompareTag("Player"))
        {
            if (!_damageApplied)
            {
                float dmg = hit.collider.GetComponent<PlayerHealth>().MaxHealth;
                hit.collider.GetComponent<IGeneralTarget>().ReceiveAggression(Vector3.zero, dmg);
                _damageApplied = true;
            }
        }else
        {
            _damageApplied = false;
        }
    }
}
