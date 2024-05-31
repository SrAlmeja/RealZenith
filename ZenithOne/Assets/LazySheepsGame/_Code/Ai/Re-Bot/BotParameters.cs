using System.Collections;
using System.Collections.Generic;
using com.LazyGames.Dz.Ai;
using UnityEngine;

[CreateAssetMenu(menuName = "LazySheeps/AI/BotParameters")]

public class BotParameters : EnemyParameters
{
    [Header("Wander Variables")]
    [Tooltip("Radius of the circle, also its distance from the npc")]
    public float circleRadius = 3f;
    [Tooltip("The range that the angle cam move along the circles' diameter")]
    public float deviationRange = 3f;
    [Tooltip("Bottom range of the time the npc remains still or moving")]
    public float minActTime = 5f;
    [Tooltip("Top range of the time the npc remains still or moving")]
    public float maxActTime = 20f;

    [Header("Dispenser Settings")] 
    [SerializeField] private float dispenserCooldown;

}
