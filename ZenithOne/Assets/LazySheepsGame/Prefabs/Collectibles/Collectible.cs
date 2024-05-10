using NaughtyAttributes;
using Obvious.Soap;
using Autohand;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Settings")]

    [Tooltip("This should be true for the special collectibles that you can only grab once in the game")]
    [SerializeField] private bool _specialCollectible;
    [Tooltip("If false, it will try to access the Grabbable Component and subscribe to the OnSqueeze event")]
    [SerializeField] private bool _autoCollectOnCollision;

    [ShowIf("_autoCollectOnCollision")]
    [SerializeField] private LayerMask _playerLayer;

    [HideIf("_specialCollectible")]
    [SerializeField] private IntVariable _itemVariable;
    [ShowIf("_specialCollectible")]
    [SerializeField] private BoolVariable _specialItemVariable;

    private void OnEnable()
    {
        if(_specialCollectible)
        {
            _specialItemVariable.Load();
            if(_specialItemVariable.Value)
            {
                gameObject.SetActive(false);
            }
        }

        if(!_autoCollectOnCollision)
        {
            Grabbable grabbable = GetComponent<Grabbable>();
            if(grabbable != null)
            {
                grabbable.OnSqueezeEvent += Collect;
            }
        }
    }

    private void OnDisable()
    {
        if (!_autoCollectOnCollision)
        {
            Grabbable grabbable = GetComponent<Grabbable>();
            if (grabbable != null)
            {
                grabbable.OnSqueezeEvent -= Collect;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_autoCollectOnCollision)
        {
            if(_playerLayer == (_playerLayer | (1 << other.gameObject.layer)))
            {
                Collect();
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_autoCollectOnCollision)
        {
            if (_playerLayer == (_playerLayer | (1 << collision.gameObject.layer)))
            {
                Collect();
                gameObject.SetActive(false);
            }
        }
    }

    private void Collect()
    {
        SaveInfo();
    }

    private void Collect(Hand hand, Grabbable grab)
    {
        SaveInfo();
    }

    private void SaveInfo()
    {
        if (_specialCollectible)
        {
            _specialItemVariable.Value = true;
            _specialItemVariable.Save();
        }
        else
        {
            _itemVariable.Value++;
        }
    }
}
