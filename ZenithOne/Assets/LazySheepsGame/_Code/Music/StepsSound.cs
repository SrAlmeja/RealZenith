using Autohand;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class StepsSound : MonoBehaviour
{
    public InputActionProperty moveAxis;

    [SerializeField] private StudioEventEmitter stepsSound;
    [SerializeField, Tooltip("Cooldown time in seconds")] private float cooldownTime = 1.0f; // Tiempo de cooldown configurable desde el editor

    private float _elapsedTime = 0f;
    private bool isMoving = false;
    private Vector2 axis;

    public bool IsMoving => isMoving;
    
    private void OnEnable() {
        if (moveAxis.action != null) moveAxis.action.Enable();
        if (moveAxis.action != null) moveAxis.action.performed += MoveAction;
    }

    private void OnDisable() {
        if (moveAxis.action != null) moveAxis.action.performed -= MoveAction;
    }
    

    void MoveAction(InputAction.CallbackContext a) {
         axis = a.ReadValue<Vector2>();
    }
    
    private void Update()
    {
        if (moveAxis != null)
        { 
            axis = moveAxis.action.ReadValue<Vector2>(); // Obtener el valor del eje de movimiento
            if (axis != Vector2.zero) // Si el jugador se está moviendo
            {
                _elapsedTime += Time.deltaTime; // Incrementar el tiempo transcurrido

                if (_elapsedTime >= cooldownTime)
                {
                    stepsSound.Play();
                    _elapsedTime = 0f;
                }
            }
            else
            {
                stepsSound.Stop();
                _elapsedTime = 0f; // Resetear el tiempo transcurrido si el jugador no se está moviendo
            }
        }
    }
}