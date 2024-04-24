using com.LazyGames.Dz.Ai;
using UnityEditor;
using UnityEngine;
using CryoStorage;

public class EnemyVision : MonoBehaviour
{
    public EnemyParameters Parameters => _parameters;
    
    [SerializeField] private LayerMask playerLayer;
    
    private bool _playerDetected;
    private Vector3 _targetPos;
    private EnemyParameters _parameters;
    
    private void Update()
    {
        if(_playerDetected)
            TrackPlayer();
        else
            ScanForPlayer();
    }

    private void ScanForPlayer()
    {
        if (_playerDetected) return;
        
        var colliders = Physics.OverlapSphere(transform.position, _parameters.hardDetectionRange, playerLayer);
        if (colliders.Length <= 0) return;
        
        var targetPos = colliders[0].transform;
        _targetPos = targetPos.position;
        var targetDir = (_targetPos - transform.position).normalized;
        if (!(Vector3.Angle(transform.forward, targetDir) < _parameters.coneAngle / 2)) return;
        
        var dist = Vector3.Distance(transform.position, _targetPos);
        if (!Physics.Raycast(transform.position, targetDir, out var hit, dist)) return;

        if (!hit.collider.CompareTag("Player")) return;
        
        _playerDetected = true;
    }

    private void TrackPlayer()
    {
        if(!_playerDetected) return;
        
        var viewPos = transform.position + _parameters.heightOffset;
        var targetDir = (_targetPos - viewPos).normalized;
        var dist = Vector3.Distance(viewPos, _targetPos);
        Debug.DrawRay(viewPos, targetDir * dist, Color.red);
        if (Physics.Raycast(transform.position + _parameters.heightOffset, targetDir, dist)) return;
        
        _playerDetected = false;
    }
    
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var position = transform.position + _parameters.heightOffset;
            
        Handles.color = Color.white;
        Handles.DrawWireDisc(position, Vector3.up, _parameters.hardDetectionRange);
        
        var eulerAngles = transform.eulerAngles;
        Vector3 viewAngle01 = CryoMath.DirFromAngle(eulerAngles.y, -_parameters.coneAngle / 2);
        Vector3 viewAngle02 = CryoMath.DirFromAngle(eulerAngles.y, _parameters.coneAngle / 2);
        
        Handles.color = Color.yellow;
        Handles.DrawLine(position, position + viewAngle01 * _parameters.hardDetectionRange);
        Handles.DrawLine(position, position + viewAngle02 * _parameters.hardDetectionRange);
    }
#endif
}
