using com.LazyGames;
using Lean.Pool;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Autohand;

public class WaterGranade : MonoBehaviour
{
    [Header("Dependencies")]
    [Required][SerializeField] private GameObject _button;
    [Required][SerializeField] private GameObject _waterReservoir;
    [Required][SerializeField] private GameObject _explosionVFX;
    [SerializeField] private float _vfxDuration = 1f;

    [Header("Settings")]
    [SerializeField] private LayerMask _interactWithLayers;
    [SerializeField] private TypeOfGadget _typeOfGadget;
    [SerializeField] private float _interactionRadius = 1f;
    [SerializeField] private float _maxDistance = 1f;
    [SerializeField] private float _activationTime = 1f;
    [SerializeField] private float _despawnTime = 1f;

    [Header("Sound Events")]
    [SerializeField] private UnityEvent _onActivate;
    [SerializeField] private UnityEvent _onThrow;
    [SerializeField] private UnityEvent _onExplode;

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
        _button.transform.DOLocalMoveY(2.2f, _activationTime);
        _waterReservoir.transform.DORotate(new Vector3(-90,0,360), _activationTime);
        _isActive = true;
        _onActivate?.Invoke();
    }

    public void ThrowGranade()
    {
        Debug.Log("Throw magnitude" + gameObject.GetComponent<Grabbable>().body.velocity.magnitude);
        if (gameObject.GetComponent<Grabbable>().body.velocity.magnitude > 3f)
        {
            _onThrow?.Invoke();
        }
    }

    [Button("Deactivate Granade Test")]
    private void DeactivateGranade()
    {
        _button.transform.DOLocalMoveY(1f, 0f);
        _waterReservoir.transform.DORotate(new Vector3(-90, 0, 0), 0f);
        _isActive = false;
    }

    private void ExplodeSphereCast()
    {
        _onExplode?.Invoke();
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
