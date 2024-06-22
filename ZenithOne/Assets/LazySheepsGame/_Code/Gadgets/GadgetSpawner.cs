using Lean.Pool;
using UnityEngine;

public class GadgetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private float floorOffset = .2f;
    [SerializeField] private float launchForce = 5f;
    [SerializeField] private LayerMask layerMask;

    private Rigidbody _rb;
    private TrackedCollectible _trackedCollectible;
    
    private void OnEnable()
    {
        if (_rb == null && _trackedCollectible == null)
        {
            _rb = GetComponent<Rigidbody>();
            _trackedCollectible = GetComponent<TrackedCollectible>();
        }
        _rb.velocity = Vector3.zero;
        var launchDirection = new Vector3(0, transform.up.y *2, transform.forward.z);
        Debug.Log(transform.forward.y);
        _rb.AddForce(launchDirection * launchForce, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other)
    {
        _trackedCollectible.ParentList.Remove(_trackedCollectible);
        var offset = new Vector3(0, floorOffset, 0);
        var go =LeanPool.Spawn(pickupPrefab, transform.position+offset, Quaternion.identity);
        var activeCollectible = go.GetComponent<TrackedCollectible>();
        activeCollectible.ParentList = _trackedCollectible.ParentList;
        _trackedCollectible.ParentList.Add(activeCollectible);
        LeanPool.Despawn(gameObject);
    }
}
