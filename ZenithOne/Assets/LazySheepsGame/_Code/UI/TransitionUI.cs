using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Obvious.Soap;
using UnityEngine;

public class TransitionUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _transitionDuration = 1f;
    [SerializeField] private ScriptableEventNoParam onPlayerDeathEvent;

    private void Start()
    {
        onPlayerDeathEvent.OnRaised += FadeIn;
        FadeOut();
    }


    public void FadeIn()
    {
        _canvasGroup.DOFade(1, _transitionDuration);
    }
    public void FadeOut()
    {
        _canvasGroup.DOFade(0, _transitionDuration);
    }
}

