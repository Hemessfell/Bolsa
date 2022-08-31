using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject popUpPanel;

    public TextMeshProUGUI texto_trabalho, texto_Qq, texto_Qf, texto_N;

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
	canClickOnPieces = true;
    }
    #endregion

    public void PlayButtonMethod()
    {
        popUpPanel.transform.DOLocalMoveY(-75.0f, 0.75f).SetUpdate(true);
        popUpPanel.transform.DOLocalMoveY(0.0f, 0.25f).SetDelay(0.75f).SetUpdate(true);
        canClickOnPieces = false;
        canClickOnPopUp = true;

        if (!MachineManager.Instance.AreAllPiecesGlued())
        {
            popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<color=\"red\">" + "Voc� Precisa Anexar Todas as Pe�as!" + "</color>";
            Debug.Log("need to glue more");
        }
        else
        {
            if (!MachineManager.Instance.IsThereASourcePieceGlued())
            {
                popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<color=\"red\">" + "Voc� Precisa Anexar a Pe�a Fonte!" + "</color>";
                Debug.Log("glue source piece");
                return;
            }

            if (MachineManager.Instance.AreAllPiecesCorrectlyPlaced())
            {
                string text = "";

                if(MachineManager.Instance.SourcePieceName() == "Carv�o")
                    text = "<color=\"green\">" + "A M�quina Ligou!" + "</color>";
                else if(MachineManager.Instance.SourcePieceName() == "Lenha")
                    text = "<color=\"red\">" + "Fonte de Calor Fraca. A M�quina N�o Ligou!" + "</color>";
                else if(MachineManager.Instance.SourcePieceName() == "Oil")
                    text = "<color=\"red\">" + "Fonte de Calor Muito Forte. A M�quina Quebrou!" + "</color>";
                Debug.Log("youre right");

                popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = text;
            }
            else
            {
                popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<color=\"red\">" + "Voc� Errou A Ordem das Pe�as!" + "</color>";
                Debug.Log("youre wrong");
            }
        }
    }

    public void RectractButtonMethod()
    {
        popUpPanel.transform.DOLocalMoveY(787.0f, 0.75f).OnComplete(() => canClickOnPieces = true).SetUpdate(true);
        canClickOnPopUp = false;
        Time.timeScale = 1.0f;
    }
}