using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;

    private bool isLoading;

    #region "Singleton" Logic
    private static SceneLoader instance;
    public static SceneLoader Instnace
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion

    private void Start()
    {
        group.DOFade(1.0f, 0.0f);
        group.DOFade(0.0f, 1.0f);
    }

    public void LoadSceneByIndex(int index)
    {
        if (!isLoading)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(group.DOFade(1.0f, 1.0f).OnComplete(() => SceneManager.LoadScene(index))).AppendCallback(() => isLoading = false)
                .Append(group.DOFade(0.0f, 1.0f));
            isLoading = true;
        }
    }

    public void ReloadScene()
    {
        if (!isLoading)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(group.DOFade(1.0f, 1.0f).OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex))).AppendCallback(() => isLoading = false)
                .Append(group.DOFade(0.0f, 1.0f));
            isLoading = true;
        }
    }

    public void LoadNextScene()
    {
        if (!isLoading)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(group.DOFade(1.0f, 1.0f).OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1))).AppendCallback(() => isLoading = false)
                .Append(group.DOFade(0.0f, 1.0f));
            isLoading = true;

            UIManager.Instance.SetNextSceneButton(false);
        }
    }
}
