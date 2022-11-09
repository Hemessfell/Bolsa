using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject popUpPanel, trainTimerPanel, runMachineButton, trainUI, generalTexts, mainMenu, nextSceneButton;
    [SerializeField] private GameObject[] playButtons;

    public TextMeshProUGUI texto_trabalho, texto_Qq, texto_Qf, texto_N, text_Potencia, coals, trainTimer;
    [SerializeField] TextMeshProUGUI fontesDeCalor, pecas, path;

    [SerializeField] private Slider pathSlider;

    [SerializeField] private float pathSliderMaxValue;

    public string trabalhoTxt, QqTxt, QfTxt, NTxt;

    public bool canClickOnPieces, canUpdatePathSlider, canUpdateTrainTimer;
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
        TurnTextIntoDots();
    }
    #endregion

    private void Update()
    {
        UpdatePathSlider();
        UpdateTrainTimer();
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
                    SetNextSceneButton(true);
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
        generalTexts.SetActive(true);
        runMachineButton.SetActive(true);
        fontesDeCalor.gameObject.SetActive(true);
        pecas.gameObject.SetActive(true);
        path.gameObject.SetActive(false);
        trainUI.SetActive(false);
        coals.gameObject.SetActive(false);
        mainMenu.SetActive(false);
        pathSlider.value = 0.0f;
    }

    public void LoadSecondScene()
    {
        generalTexts.SetActive(true);
        runMachineButton.SetActive(false);
        fontesDeCalor.gameObject.SetActive(false);
        pecas.gameObject.SetActive(false);
        trainUI.SetActive(true);
        path.gameObject.SetActive(true);
        coals.gameObject.SetActive(true);
        mainMenu.SetActive(false);
        pathSlider.maxValue = pathSliderMaxValue;
        pathSlider.value = 0.0f;
    }

    public void LoadMainMenu()
    {
        generalTexts.SetActive(false);
        mainMenu.SetActive(true);
        runMachineButton.SetActive(false);
        fontesDeCalor.gameObject.SetActive(false);
        pecas.gameObject.SetActive(false);
        path.gameObject.SetActive(false);
        trainUI.SetActive(false);
        coals.gameObject.SetActive(false);
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

    public void TurnTextIntoDots()
    {
        trabalhoTxt = "--------";
        QqTxt = "--------";
        QfTxt = "--------";
        NTxt = "--------";
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetNextSceneButton(bool willActivate)
    {
        if (willActivate)
        {
            nextSceneButton.GetComponent<RectTransform>().DOLocalMove(new Vector3(-1018.0f, 465.0f), 0.50f);
        }
        else
        {
            nextSceneButton.GetComponent<RectTransform>().DOLocalMove(new Vector3(-1018.0f, 790.0f), 0.50f);
        }

        TurnTextIntoDots();
    }

    private void UpdateTrainTimer()
    {
        if (canUpdateTrainTimer)
        {
            trainTimer.text = Mathf.Floor(MachineManager.Instance.trainTimer / 60).ToString("00") + ":" + Mathf.FloorToInt(MachineManager.Instance.trainTimer % 60).ToString("00");
            MachineManager.Instance.trainTimer -= Time.deltaTime;

            if(MachineManager.Instance.trainTimer <= 0.0f)
            {
                SetTrainTimerPanel();
                canUpdateTrainTimer = false;
            }

            MachineManager.Instance.trainTimer = Mathf.Clamp(MachineManager.Instance.trainTimer, 0.0f, MachineManager.Instance.trainTimerMax);
        }
    }

    private void SetTrainTimerPanel()
    {
        trainTimerPanel.transform.DOLocalMoveY(-75.0f, 0.75f).SetUpdate(true);
        trainTimerPanel.transform.DOLocalMoveY(0.0f, 0.25f).SetDelay(0.75f).SetUpdate(true);
        canClickOnPieces = false;
        canClickOnPopUp = true;
    }

    public void RetractTrainTimerPanel()
    {
        trainTimerPanel.transform.DOLocalMoveY(787.0f, 0.75f).OnComplete(() => canClickOnPieces = true).SetUpdate(true);
        canClickOnPopUp = false;
        Time.timeScale = 1.0f;
        SceneLoader.Instnace.ReloadScene();
    }
}