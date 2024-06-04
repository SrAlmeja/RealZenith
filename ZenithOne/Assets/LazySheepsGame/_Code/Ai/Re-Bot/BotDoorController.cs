using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDoorController : MonoBehaviour
{
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private float lerpDuration = 1.0f;
    [SerializeField] private float targetRotation = -255;
    private float _startRotation;
    
    public void Start()
    {
        _startRotation = door1.transform.localEulerAngles.y;
        
    }
    
    public void OpenDoor()
    {
        StartCoroutine(CorLerpDoor(door1.transform.localEulerAngles, new Vector3(targetRotation,0 , 0), lerpDuration));
    }
    
    public void CloseDoor()
    {
        StartCoroutine(CorLerpDoor(door1.transform.localEulerAngles, new Vector3(-90, 0, 0), lerpDuration));
    }

    private IEnumerator CorLerpDoor(Vector3 startRotation, Vector3 endRotation, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            door1.transform.localEulerAngles = Vector3.Lerp(startRotation, endRotation, time / duration);
            door2.transform.localEulerAngles = Vector3.Lerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
