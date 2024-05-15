using com.LazyGames.Dz.Ai;
using UnityEditor;
using UnityEngine;
using CryoStorage;

public class EnemyVision : MonoBehaviour
{
    public EnemyParameters Parameters { get; private set; }

    [SerializeField] private Transform headTransform;
    private LayerMask _playerLayer;
    
    private bool _playerDetected;
    private Transform _target;
    private EnemyBt _parentBt;
    

    public void Initialize(EnemyBt parentBt, EnemyParameters parameters, LayerMask playerLayer)
    {
        _parentBt = parentBt;
        Parameters = parameters;
        _playerLayer = playerLayer;
    }
    
    public void Step()
    {
        if (_playerDetected)
        {
            TrackPlayer();
        }
        else
        {
            ScanForPlayer();
        }
    }

    private void ScanForPlayer()
    {
        if (_playerDetected) return;
        var colliders = Physics.OverlapSphere(transform.position, Parameters.detectionRange, _playerLayer);
        if (colliders.Length <= 0) return;
        
        _target = colliders[0].transform;
        var targetDir = (_target.transform.position - transform.position).normalized;
        if (!(Vector3.Angle(headTransform.forward, targetDir) < Parameters.coneAngle / 2)) return;
        
        var dist = Vector3.Distance(transform.position, _target.position);
        if (!Physics.Raycast(transform.position, targetDir, out var hit, dist)) return;

        if (!hit.collider.CompareTag("Player")) return;
        _parentBt.PlayerDetected(_target);
        // Debug.Log("Player Detected");
        _playerDetected = true;
    }

    private void TrackPlayer()
    {
        if (!_playerDetected) return;
        var viewPos = transform.position + Parameters.heightOffset;
        var targetDir = (_target.position - viewPos).normalized;
        var dist = Vector3.Distance(viewPos, _target.position);
        var lastKnownPos = _target.position;
        Debug.DrawRay(viewPos, targetDir * dist, Color.red);
        if (!Physics.Raycast(viewPos, targetDir, out var hit, dist)) return;
        if (hit.collider.CompareTag("Player")) return;
        _parentBt.PlayerLost(lastKnownPos);
        // Debug.Log("Player Lost");
        _playerDetected = false;
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(Parameters == null) return;
        var position = transform.position + Parameters.heightOffset;
        Handles.color = Color.white;
        Handles.DrawWireDisc(position, Vector3.up, Parameters.detectionRange);
        
        var eulerAngles = headTransform.eulerAngles;
        Vector3 viewAngle01 = CryoMath.DirFromAngle(eulerAngles.y, -Parameters.coneAngle / 2);
        Vector3 viewAngle02 = CryoMath.DirFromAngle(eulerAngles.y, Parameters.coneAngle / 2);
        
        Handles.color = Color.yellow;
        Handles.DrawLine(position, position + viewAngle01 * Parameters.detectionRange);
        Handles.DrawLine(position, position + viewAngle02 * Parameters.detectionRange);
    }
#endif
}
