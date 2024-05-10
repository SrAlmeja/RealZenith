// Creado Raymundo Mosqueda 09/05/24

using Obvious.Soap;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.LazyGames.Dz
{
    public class SceneTrigger : MonoBehaviour
    {
        [SerializeField] private float delay;
        [SerializeField] private SceneAsset sceneToLoad;
        [SerializeField] private ScriptableEventNoParam onSceneLoad;
        // [SerializeField] private ScriptableEventNoParam onTransitionEvent;

        
        private void Start()
        {
            var col =gameObject.AddComponent<BoxCollider>();
            col.isTrigger = true;
            col.size = transform.lossyScale;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            onSceneLoad.Raise();
            Invoke(nameof(Load), delay);
        }

        private void Load()
        {
            SceneManager.LoadScene(sceneToLoad.name);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, transform.lossyScale *2);
        }
    }
}
