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
    }
    #endregion

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        UIManager.Instance.canUpdateTrainTimer = false;

        if (scene.name == "MainMenu")
        {
            UIManager.Instance.LoadMainMenu();
        }

        if (scene.name.Contains("Level"))
        {
            string text = "";
            UIManager.Instance.PlayButtonMethod(50);
            Time.timeScale = 0.0f;

            if (scene.name.StartsWith("First"))
            {
                text = "<color=\"black\">" + "Nesse primeiro desafio, você terá que encaixar as peças no lugar certo e escolher a fonte de calor correta para fazer " +
                    "a máquina funcionar!" + "</color>";
                UIManager.Instance.UpdatePlayButtons(0);
                UIManager.Instance.LoadFirstScene();
            }else if (scene.name.StartsWith("Second"))
            {
                UIManager.Instance.LoadSecondScene();
                MachineManager.Instance.trainTimer = MachineManager.Instance.trainTimerMax;
                UIManager.Instance.canUpdateTrainTimer = true;
                text = "<color=\"black\">" + "Nessa segunda fase, você terá que levar o trem até o fim do caminho!";
            }

            UIManager.Instance.UpdateStartLevelText(text);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
