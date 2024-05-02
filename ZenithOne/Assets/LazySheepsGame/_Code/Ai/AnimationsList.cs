using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationsList", menuName = "LazySheeps/AI/AnimationsList")]
public class AnimationsList : ScriptableObject
{
    public List<AnimationClip> animations;
}
