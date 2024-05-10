using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform objectToTrack; // Objeto que queremos seguir

    void Update()
    {
        if (objectToTrack != null)
        {
            // Obtener la posición actual del objeto que queremos seguir
            Vector3 targetPosition = objectToTrack.position;

            // Actualizar la posición del objeto que sigue al objeto que queremos rastrear
            transform.position = targetPosition;
        }
    }
}
