using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// File contains Editor class for EnemyWayPoints
namespace com.LazyGames.Dz.Ai
{
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
        
        Transform[] GetChildTransform (Transform T)
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform child in T)
            {
                children.Add(child);
            }
            return children.ToArray();
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
                }
            }
        }
    }
}

