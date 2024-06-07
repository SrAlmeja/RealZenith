using Lean.Pool;
using UnityEngine;

public class GadgetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private float floorOffset = .2f;
    [SerializeField] private float launchForce = 5f;
    [SerializeField] private LayerMask layerMask;

    private Rigidbody _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        var launchDirection = new Vector3(0, transform.forward.y, transform.forward.z);
        _rb.AddForce(launchDirection * launchForce, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other)
    {
        var offset = new Vector3(0, floorOffset, 0);
        LeanPool.Spawn(pickupPrefab, transform.position+offset, Quaternion.identity);
        LeanPool.Despawn(gameObject);
    }
}
