using NaughtyAttributes;
using Obvious.Soap;
using System.Collections.Generic;
using UnityEngine;

public class OmnitrixKnobVFXManager : MonoBehaviour
{
    [Header("Dependencies")]
    [Required][SerializeField] private ScriptableEventInt _omnitrixGadgetChannel;
    [Required][SerializeField] private Material _activatedKnobMaterial;
    [Required][SerializeField] private Material _deactivatedKnobMaterial;
    [SerializeField] private List<GameObject> _knobPoints;

    private int _activeKnob = 3;

    private void OnEnable()
    {
        UpdateKnobs(_activeKnob);
        _omnitrixGadgetChannel.OnRaised += UpdateKnobs;
    }


    private void OnDisable()
    {
        _omnitrixGadgetChannel.OnRaised -= UpdateKnobs;
    }
    private void UpdateKnobs(int currentKnob)
    {
        if(_activeKnob == currentKnob) return;
        _activeKnob = currentKnob;
        if (currentKnob >= _knobPoints.Count || currentKnob < 0)
        {
            DeactivateAllKnobs();
            return;
        }


        for (int i = 0; i < _knobPoints.Count; i++)
        {
            if (i <= currentKnob)
            {
                _knobPoints[i].GetComponent<Renderer>().material = _activatedKnobMaterial;
            }
            else
            {
                _knobPoints[i].GetComponent<Renderer>().material = _deactivatedKnobMaterial;
            }
        }

    }

    private void DeactivateAllKnobs()
    {
        foreach (var knob in _knobPoints)
        {
            knob.GetComponent<Renderer>().material = _deactivatedKnobMaterial;
        }
    }
}
