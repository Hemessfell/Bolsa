using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject popUpPanel, runMachineButton, trainUI;
    [SerializeField] private GameObject[] playButtons;

    public TextMeshProUGUI texto_trabalho, texto_Qq, texto_Qf, texto_N, text_Potencia, coals;
    [SerializeField] TextMeshProUGUI fontesDeCalor, pecas, path;

    [SerializeField] private Slider pathSlider;

    [SerializeField] private float pathSliderMaxValue;

    public bool canClickOnPieces, canUpdatePathSlider;
    private bool canClickOnPopUp;

    public int maxCoalsAmount;
    private int currentCoalsAmount;

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
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        canClickOnPieces = true;
        currentCoalsAmount = maxCoalsAmount;
        coals.text = currentCoalsAmount + "/" + maxCoalsAmount;
    }
    #endregion

    private void Update()
    {
        UpdatePathSlider();
    }

    public void PlayButtonMethod(int fontSize)
    {
        popUpPanel.transform.DOLocalMoveY(-75.0f, 0.75f).SetUpdate(true);
        popUpPanel.transform.DOLocalMoveY(0.0f, 0.25f).SetDelay(0.75f).SetUpdate(true);
        popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().fontSize = fontSize;
        canClickOnPieces = false;
        canClickOnPopUp = true;
    }

    public void RectractButtonMethod()
    {
        popUpPanel.transform.DOLocalMoveY(787.0f, 0.75f).OnComplete(() => canClickOnPieces = true).SetUpdate(true);
        canClickOnPopUp = false;
        Time.timeScale = 1.0f;
    }

    public void UpdateFirstMachineText()
    {
        if (!MachineManager.Instance.AreAllPiecesGlued())
        {
            popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<color=\"red\">" + "Você Precisa Anexar Todas as Peças!" + "</color>";
        }
        else
        {
            if (!MachineManager.Instance.IsThereASourcePieceGlued())
            {
                popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<color=\"red\">" + "Você Precisa Anexar a Peça Fonte!" + "</color>";
                return;
            }

            if (MachineManager.Instance.AreAllPiecesCorrectlyPlaced())
            {
                string text = "";

                if (MachineManager.Instance.SourcePieceName() == "Carvão")
                {
                    MachineManager.Instance.machineIsFunctioning = true;
                    text = "<color=\"green\">" + "A Máquina Ligou!" + "</color>";
                }
                else if (MachineManager.Instance.SourcePieceName() == "Lenha")
                    text = "<color=\"red\">" + "Fonte de Calor Fraca. A Máquina Não Ligou!" + "</color>";
                else if (MachineManager.Instance.SourcePieceName() == "Oil")
                    text = "<color=\"red\">" + "Fonte de Calor Muito Forte. A Máquina Quebrou!" + "</color>";

                popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = text;
            }
            else
            {
                popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "<color=\"red\">" + "Você Errou A Ordem das Peças!" + "</color>";
            }
        }
    }

    public void UpdateStartLevelText(string text)
    {
        popUpPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void UpdatePlayButtons(int index)
    {
        for (int i = 0; i < playButtons.Length; i++)
        {
            playButtons[i].SetActive(false);
        }

        playButtons[index].SetActive(true);
    }

    public void LoadFirstScene()
    {
        runMachineButton.SetActive(true);
        fontesDeCalor.gameObject.SetActive(true);
        pecas.gameObject.SetActive(true);
        path.gameObject.SetActive(false);
        trainUI.SetActive(false);
        coals.gameObject.SetActive(false);
        pathSlider.value = 0.0f;
    }

    public void LoadSecondScene()
    {
        runMachineButton.SetActive(false);
        fontesDeCalor.gameObject.SetActive(false);
        pecas.gameObject.SetActive(false);
        trainUI.SetActive(true);
        path.gameObject.SetActive(true);
        coals.gameObject.SetActive(true);
        pathSlider.maxValue = pathSliderMaxValue;
        pathSlider.value = 0.0f;
    }

    private void UpdatePathSlider()
    {
        if (canUpdatePathSlider)
        {
            pathSlider.value += 1 * Time.deltaTime;
        }
    }

    public void UpdateCoalsCounter(bool willIncrease)
    {
        if (willIncrease)
            currentCoalsAmount++;
        else
            currentCoalsAmount--;
        coals.text = currentCoalsAmount + "/" + maxCoalsAmount;
    }
}