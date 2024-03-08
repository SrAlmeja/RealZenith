using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// File contains Editor class for EnemyWayPoints
namespace com.LazyGames.Dz.Ai
{
    public class EnemyWayPoints : MonoBehaviour
    {
        public Transform[] WayPoints => _wayPoints;
        private Transform[] _wayPoints;

        private void OnEnable()
        {
            BuildArray();
        }

        public void BuildArray()
        {
            _wayPoints = GetChildTransform(transform);
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
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(_wayPoints[i].position, 0.1f);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(_wayPoints[i].position,
                    i < _wayPoints.Length - 1 ? _wayPoints[i + 1].position : _wayPoints[0].position);
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

