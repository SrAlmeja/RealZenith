using UnityEngine;

public class SimpleCollect : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask _playerLayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (_playerLayer == (_playerLayer | (1 << collision.gameObject.layer)))
        {
            Collect();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_playerLayer == (_playerLayer | (1 << other.gameObject.layer)))
        {
            Collect();
            gameObject.SetActive(false);
        }
    }

    private void Collect()
    {
        Debug.Log("Collected " + this.gameObject.name);
    }
}
