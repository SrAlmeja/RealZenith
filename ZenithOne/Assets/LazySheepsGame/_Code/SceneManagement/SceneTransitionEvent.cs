using Obvious.Soap;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionEvent : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private ScriptableEventNoParam onSceneLoad;
    void Start()
    {
        onSceneLoad.Raise();
        Invoke(nameof(Load), delay);
    }
    
    private void Load()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
