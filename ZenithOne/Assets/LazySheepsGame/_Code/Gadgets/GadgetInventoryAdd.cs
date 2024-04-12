using UnityEngine;
using Obvious.Soap;
using NaughtyAttributes;
using Lean.Pool;

public class GadgetInventoryAdd : MonoBehaviour
{
    [Header("Dependencies")]
    [Required][SerializeField] private IntVariable _gadgetToAdd;

    [Header("Settings")]
    [SerializeField] private int _valueToAdd;
    [Tooltip("If true, it will hard set the gadget to that value. If false, it adds the value to the existing value.")]
    [SerializeField] private bool _hardSetValue = false;
    [Tooltip("If true, it will call LeanPool to despwan the object instead of Destroy()")]
    [SerializeField] private bool _poolOnDestroy = false;
    [Tooltip("If false, it will ignore on collision and on trigger enter.")]
    [SerializeField] private bool _worksOnCollisions = false;
    [ShowIf("_worksOnCollisions")][SerializeField] private LayerMask _collisionLayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (!_worksOnCollisions) return;
        if ((_collisionLayer.value & 1 << collision.gameObject.layer) != 0)
        {
            AddValue();
            Despawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_worksOnCollisions) return;
        if ((_collisionLayer.value & 1 << other.gameObject.layer) != 0)
        {
            AddValue();
            Despawn();
        }
    }

    public void GrabbItem()
    {
        AddValue();
    }

    private void AddValue()
    {
        if (_hardSetValue) _gadgetToAdd.Value = _valueToAdd;
        else _gadgetToAdd.Value += _valueToAdd;
    }

    private void Despawn()
    {
        if (_poolOnDestroy) LeanPool.Despawn(this.gameObject);
        else Destroy(gameObject);
    }
}
