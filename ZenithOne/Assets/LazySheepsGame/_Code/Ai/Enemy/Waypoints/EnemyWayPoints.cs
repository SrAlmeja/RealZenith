using UnityEditor;
using UnityEngine;

// File contains Editor class for EnemyWayPoints
namespace com.LazyGames.Dz.Ai
{
    [SelectionBase]
    public class EnemyWayPoints : MonoBehaviour
    {
        public Waypoint[] WayPoints =>wayPoints;
        
        [SerializeReference] private Waypoint[] wayPoints;

        private void OnEnable()
        {
            BuildArray();
        }

        public void BuildArray()
        {
            wayPoints = GetComponentsInChildren<Waypoint>();
        }

        private void OnDrawGizmos()
        {
            if (wayPoints == null) return;
            for (int i = 0; i < wayPoints.Length; i++)
            {
                Transform t = wayPoints[i].transform;
                Gizmos.color = Color.white;
                Gizmos.DrawLine(wayPoints[i].gameObject.transform.position, i < wayPoints.Length
                    - 1 ? wayPoints[i + 1].gameObject.transform.position : wayPoints[0].gameObject.transform.position);
            }
        }
    }

   #if UNITY_EDITOR
    [CustomEditor(typeof(EnemyWayPoints))]
    public class EnemyWaypointsEditor : Editor
    {
        private EnemyWayPoints _enemyWayPoints;
        
        SerializedProperty _wayPoints;
        
        private void OnEnable()
        {
            _enemyWayPoints = (EnemyWayPoints)target;
            _wayPoints = serializedObject.FindProperty("wayPoints");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update(); // Ensure the serialized object is up-to-date

            // base.OnInspectorGUI();
    
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                if (GUILayout.Button("Visualize Waypoints", GUILayout.Height(35)))
                {
                    _enemyWayPoints.BuildArray();
                    Debug.Log($"Displaying {_enemyWayPoints.WayPoints.Length} waypoints");
                }

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Add Waypoint", GUILayout.Height(35)))
                    {
                        int count = _wayPoints.arraySize;
                        GameObject go = new GameObject("Waypoint");
                        go.transform.SetParent(_enemyWayPoints.transform);
                        go.transform.position = _enemyWayPoints.transform.position;
                        Waypoint waypointComponent = go.AddComponent<Waypoint>();
                        waypointComponent.WaitTime = 0.3f; // Set default wait time
                        go.name = $"Waypoint {count}";
                        _enemyWayPoints.BuildArray();
                    }

                    if (GUILayout.Button("Remove Waypoint", GUILayout.Height(35)))
                    {
                        if (_wayPoints.arraySize > 0)
                        {
                            DestroyImmediate(_wayPoints.GetArrayElementAtIndex(_wayPoints.arraySize - 1).objectReferenceValue);
                            _enemyWayPoints.BuildArray();
                        }
                    }
                }

                GUILayout.FlexibleSpace();

                if (_wayPoints.arraySize > 0)
                {
                    for (int i = 0; i < _wayPoints.arraySize; i++)
                    {
                        SerializedProperty waypointProperty = _wayPoints.GetArrayElementAtIndex(i);
                        Debug.Log(waypointProperty.type);
                        SerializedProperty waitTimeProperty = waypointProperty.serializedObject.FindProperty("waitTime");
                        Debug.Log(waitTimeProperty);
                        EditorGUILayout.PropertyField(waypointProperty, new GUIContent($"Waypoint {i}"));
                        waitTimeProperty.floatValue = EditorGUILayout.FloatField("Wait Time", waitTimeProperty.floatValue);
                    }
                }
            }

            serializedObject.ApplyModifiedProperties(); // Apply any modified properties back to the serialized object
        }
    }
    #endif
}

