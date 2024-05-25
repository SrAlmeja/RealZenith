using com.LazyGames;
using DG.Tweening;
using Lean.Pool;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarGranade : MonoBehaviour
{
    [Header("Dependencies")]
    [Required][SerializeField] private GameObject _explosionVFX;
    [SerializeField] private float _vfxDuration = 1f;

    [Header("Settings")]
    [SerializeField] private LayerMask _interactWithLayers;
    [SerializeField] private TypeOfGadget _typeOfGadget;
    [SerializeField] private float _interactionRadius = 1f;
    [SerializeField] private float _maxDistance = 1f;
    [SerializeField] private float _activationTime = 1f;
    [SerializeField] private float _despawnTime = 1f;

    private bool _isActive = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isActive) return;
        if ((_interactWithLayers.value & 1 << collision.gameObject.layer) != 0)
        {
            ExplodeSphereCast();
            FireVFX();
        }
    }


    [Button("Activate Granade Test")]
    public void ActivateGranade()
    {
        _isActive = true;
    }

    [Button("Deactivate Granade Test")]
    private void DeactivateGranade()
    {
        _isActive = false;
    }

    private void ExplodeSphereCast()
    {
        RaycastHit[] explosionHits = Physics.SphereCastAll(transform.position, _interactionRadius, Vector3.up, _maxDistance, _interactWithLayers);

        foreach (RaycastHit hit in explosionHits)
        {
            IGadgetInteractable gadgetInteractable;

            if (hit.collider.gameObject.TryGetComponent(out gadgetInteractable))
            {
                gadgetInteractable.GadgetInteraction(_typeOfGadget);
            }
        }
    }

    private void FireVFX()
    {
        _explosionVFX.SetActive(true);
        _explosionVFX.transform.parent = null;
        _explosionVFX.GetComponent<ParticleSystem>().Play(true);
        Invoke("RestoreVFX", _vfxDuration);
    }

    private void RestoreVFX()
    {
        _explosionVFX.transform.parent = this.gameObject.transform;
        _explosionVFX.transform.localPosition = Vector3.zero;
        _explosionVFX.GetComponent<ParticleSystem>().Stop();
        _explosionVFX.SetActive(false);
        Invoke("Despawn", _despawnTime);
    }

    private void Despawn()
    {
        DeactivateGranade();
        LeanPool.Despawn(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _interactionRadius);
    }

}
