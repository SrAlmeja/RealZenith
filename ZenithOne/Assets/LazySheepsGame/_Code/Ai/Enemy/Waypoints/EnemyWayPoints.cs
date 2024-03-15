using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// File contains Editor class for EnemyWayPoints
namespace com.LazyGames.Dz.Ai
{
    [SelectionBase]
    public class EnemyWayPoints : MonoBehaviour
    {
        public Waypoint[] WayPoints => _wayPoints;
        private Waypoint[] _wayPoints;

        private void OnEnable()
        {
            BuildArray();
        }

        public void BuildArray()
        {
            _wayPoints = GetComponentsInChildren<Waypoint>();
        }

        private void OnDrawGizmos()
        {
            if (_wayPoints == null) return;
            for (int i = 0; i < _wayPoints.Length; i++)
            {
                Transform t = _wayPoints[i].transform;
                Gizmos.color = Color.white;
                Gizmos.DrawLine(_wayPoints[i].gameObject.transform.position,
                    i < _wayPoints.Length - 1 ? _wayPoints[i + 1].gameObject.transform.position : _wayPoints[0].gameObject.transform.position);
            }
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(EnemyWayPoints))]
    public class EnemyWaypointsEditor : Editor
    {
        private EnemyWayPoints _enemyWayPoints;
        private void OnEnable()
        {
            _enemyWayPoints = (EnemyWayPoints) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                if (GUILayout.Button("Visualize Waypoints", GUILayout.Height(35)))
                {
                    _enemyWayPoints.BuildArray();
                    Debug.Log($"Displaying {_enemyWayPoints.WayPoints.Length} waypoints");
                }
                
                GUILayout.FlexibleSpace();

                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    if (_enemyWayPoints.WayPoints == null) return;
                    for (int i = 0; i < _enemyWayPoints.WayPoints.Length; i++)
                    {
                        using (new GUILayout.HorizontalScope())
                        {
                            GUILayout.Label($"Waypoint {i}");
                            _enemyWayPoints.WayPoints[i] = (Waypoint) EditorGUILayout.ObjectField(_enemyWayPoints.WayPoints[i], typeof(Waypoint), true);
                            _enemyWayPoints.WayPoints[i].WaitTime = EditorGUILayout.FloatField(_enemyWayPoints.WayPoints[i].WaitTime);
                        }
                    }
                }
            }
        }
    }
    #endif
}

