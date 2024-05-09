using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

namespace com.LazyGames.Dz.Ai
{
    // File contains editor class for EnemyWayPoints
    public class EnemyWayPoints : MonoBehaviour
    {
        public List <Waypoint> WayPoints =>wayPoints;
        
        [SerializeReference] private List <Waypoint> wayPoints;

        private void OnEnable()
        {
            BuildArray();
        }

        public void BuildArray()
        {
            wayPoints = GetComponentsInChildren<Waypoint>().ToList();
        }

        private void OnDrawGizmos()
        {
            if (wayPoints == null) return;
            for (int i = 0; i < wayPoints.Count; i++)
            {
                Transform t = wayPoints[i].transform;
                Gizmos.color = Color.white;
                Gizmos.DrawLine(wayPoints[i].gameObject.transform.position, i < wayPoints.Count
                    - 1 ? wayPoints[i + 1].gameObject.transform.position : wayPoints[0].gameObject.transform.position);
            }
        }

        public void Remove(int id)
        {
            if (wayPoints.Count <= 0) return;
            wayPoints.Remove(wayPoints[id]);
            BuildArray();
        }
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(EnemyWayPoints))]
    public class EnemyWaypointsEditor : Editor
    {
        private EnemyWayPoints _enemyWayPoints;
        private SerializedProperty _wayPointsProp;

        private void OnEnable()
        {
            _enemyWayPoints = (EnemyWayPoints)target;
            _enemyWayPoints.BuildArray();
            _wayPointsProp = serializedObject.FindProperty("wayPoints");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                // if (GUILayout.Button("Visualize Waypoints", GUILayout.Height(35)))
                // {
                //     _enemyWayPoints.BuildArray();
                //     Debug.Log($"EnemyWaypoints: Displaying {_enemyWayPoints.WayPoints.Length} waypoints");
                // }

                
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Add Waypoint", GUILayout.Height(35)))
                    {
                        int count = _wayPointsProp.arraySize;
                        GameObject go = new GameObject("Waypoint");
                        go.transform.SetParent(_enemyWayPoints.transform);
                        go.transform.position = _enemyWayPoints.transform.position;
                        Waypoint waypointComponent = go.AddComponent<Waypoint>();
                        waypointComponent.WaitTime = 0.3f; // Set default wait time
                        go.name = $"Waypoint {count}";
                        _enemyWayPoints.BuildArray();
                    }
                }
                
                if (_wayPointsProp.arraySize <= 0) return;
                GUILayout.FlexibleSpace();
                
                for (int i = 0; i < _wayPointsProp.arraySize; i++)
                {
                    using(new GUILayout.VerticalScope(EditorStyles.helpBox))
                    {
                        using (new GUILayout.HorizontalScope())
                        {
                            if(GUILayout.Button("select",GUILayout.Width(50)))
                            {
                                Selection.activeObject = _wayPointsProp.GetArrayElementAtIndex(i).objectReferenceValue;
                            }
                            
                            var waypointProperty = _wayPointsProp.GetArrayElementAtIndex(i);
                            var so = new SerializedObject(waypointProperty.objectReferenceValue);
                            var waitTimeProperty = so.FindProperty("waitTime");
                            EditorGUILayout.PropertyField(waypointProperty,GUIContent.none);
                            GUILayout.Label("WaitTime: ");
                            waitTimeProperty.floatValue = EditorGUILayout.FloatField(so.FindProperty("waitTime").floatValue);
                            
                            if (GUILayout.Button("Delete", GUILayout.Width(50)))
                            {
                                if (_wayPointsProp.arraySize > 0)
                                {
                                    _enemyWayPoints.Remove(i);  
                                    
                                    DestroyImmediate(_wayPointsProp.GetArrayElementAtIndex(_wayPointsProp.arraySize - 1).objectReferenceValue.GameObject());
                                    _enemyWayPoints.BuildArray();
                                }
                            }
                            so.ApplyModifiedProperties();
                        }
                    }
                }
                
                serializedObject.ApplyModifiedProperties();

            }
        }
    }
    #endif
}

