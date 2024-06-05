using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionEvent : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private SceneAsset sceneToLoad;
    [SerializeField] private ScriptableEventNoParam onSceneLoad;
    void Start()
    {
        
    }

    public void DoTransition()
    {
        onSceneLoad.Raise();
        Invoke(nameof(Load), delay);
    }
    
    private void Load()
    {
        SceneManager.LoadScene(sceneToLoad.name);
    }
}
