using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LaserTrap))]
public class LaserTrapEditor : Editor
{
    public override void OnInspectorGUI()
     {
         base.OnInspectorGUI();
         LaserTrap laserTrap = (LaserTrap) target;
         if (GUILayout.Button("Activate Trap"))
         {
             laserTrap.SetATrapActive();
         }

         if (GUILayout.Button("Deactivate Trap"))
         {
             laserTrap.SetDeactiveTrap();
         }
     }
}
