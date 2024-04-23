using UnityEditor;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public delegate void OnVisionEventHandler(Vector3 position);
    public event OnVisionEventHandler? OnPlayerDetected;
    public event OnVisionEventHandler? OnPlayerLost;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    private void ScanForPlayer()
    {
        // scan logic
        OnPlayerDetected?.Invoke(Vector3.zero);
    }
    
    private void TrackPlayer()
    {
        // track logic
        OnPlayerLost?.Invoke(Vector3.zero);
    }
    
// #if UNITY_EDITOR
//     private void OnDrawGizmos()
//     {
//         var position = transform.position + parameters.heightOffset;
//             
//         Handles.color = Color.white;
//         Handles.DrawWireDisc(position, Vector3.up, parameters.hardDetectionRange);
//             
//         var eulerAngles = transform.eulerAngles;
//         Vector3 viewAngle01 = DirFromAngle(eulerAngles.y, -parameters.coneAngle / 2);
//         Vector3 viewAngle02 = DirFromAngle(eulerAngles.y, parameters.coneAngle / 2);
//         
//         Handles.color = Color.yellow;
//         Handles.DrawLine(position, position + viewAngle01 * parameters.hardDetectionRange);
//         Handles.DrawLine(position, position + viewAngle02 * parameters.hardDetectionRange);
//             
//         Vector3 viewAngle03 = DirFromAngle(eulerAngles.y, (-parameters.coneAngle -40f) / 2);
//         Vector3 viewAngle04 = DirFromAngle(eulerAngles.y, (parameters.coneAngle + 40f)/ 2);
//             
//         Handles.color = Color.red;
//         Handles.DrawLine(position, position + viewAngle03 * parameters.hardDetectionRange);
//         Handles.DrawLine(position, position + viewAngle04 * parameters.hardDetectionRange);
//     }
// #endif
}
