using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OmnitrixVisualTabern : MonoBehaviour
{
    [SerializeField] private GameObject _omnitrixVisual;
    void Start()
    {
        _omnitrixVisual.SetActive(false);
    }

    public void ShowOmnitrixVisual()
    {
        _omnitrixVisual.SetActive(true);
        _omnitrixVisual.transform.DOScale(Vector3.one, 0.5f);
    }
}
