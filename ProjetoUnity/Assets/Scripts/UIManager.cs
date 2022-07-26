using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject popUpPanel;

    public bool canClickOnPieces;
    private bool canClickOnPopUp;

    #region Singleton Logic
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("The UIManager instance is NULL");
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

    public void PlayButtonMethod()
    {
        popUpPanel.transform.DOLocalMoveY(-75.0f, 0.75f);
        popUpPanel.transform.DOLocalMoveY(0.0f, 0.25f).SetDelay(0.75f);
        canClickOnPieces = false;
        canClickOnPopUp = true;

        if (!MachineManager.Instance.AreAllPiecesGlued())
        {
            popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<color=\"red\">" + "Você Precisa Anexar Todas as Peças!" + "</color>";
            Debug.Log("need to glue more");
        }
        else
        {
            if (!MachineManager.Instance.AreAllSourcePiecesGlued())
            {
                popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<color=\"red\">" + "Você Precisa Anexar a Peça Fonte!" + "</color>";
                Debug.Log("glue source piece");
                return;
            }

            if (MachineManager.Instance.AreAllPiecesCorrectlyPlaced())
            {
                popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<color=\"green\">" + "Você Acertou!" + "</color>";
                Debug.Log("youre right");
            }
            else
            {
                popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<color=\"red\">" + "Você Errou!" + "</color>";
                Debug.Log("youre wrong");
            }
        }
    }

    public void RectractButtonMethod()
    {
        popUpPanel.transform.DOLocalMoveY(787.0f, 0.75f).OnComplete(() => canClickOnPieces = true);
        canClickOnPopUp = false;
    }
}