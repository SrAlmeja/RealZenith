using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimTriangles : MonoBehaviour
{
    [SerializeField] private GameObject triangle;
    void Start()
    {
        triangle.transform.DOJump(triangle.transform.position, 0.3f, 1, 0.5f).SetLoops(-1);
        triangle.SetActive(false);
        
    }
    
    
    public void ActivateTriangle()
    {
        triangle.SetActive(true);
    }
    
    public void DeactivateTriangle()
    {
        triangle.SetActive(false);
    }
}
