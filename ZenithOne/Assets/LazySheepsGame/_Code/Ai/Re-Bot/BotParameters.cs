using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LazySheeps/AI/BotParameters")]

public class BotParameters : ScriptableObject
{
    [Header("Wander Variables")] 
    public float circleRadius;
    public float circleDistance;
    public float angleChange;
    public float movementSpeed;
    
    [Header("Detection Variables")] 
    public float detectionRange;
    public float coneAngle;
    
}
