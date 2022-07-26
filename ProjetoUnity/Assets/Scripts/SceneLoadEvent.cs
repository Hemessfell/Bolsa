using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadEvent : MonoBehaviour
{
    #region Singleton Logic
    private static SceneLoadEvent instance;
    public static SceneLoadEvent Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("The SceneLoadEvent instance is NULL");
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoad;
    }
    #endregion

    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
