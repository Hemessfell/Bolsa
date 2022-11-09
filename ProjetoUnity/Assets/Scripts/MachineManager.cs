using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MachineManager : MonoBehaviour
{
    private List<PecaScript> peças = new List<PecaScript>();
    private List<PecaFonteScript> pecasFonte = new List<PecaFonteScript>();

    public List<Animator> adicionalAnimators;

    public bool machineIsFunctioning;
    private bool canUpdatePathSlider;

    public float trainTimerMax;
    [HideInInspector] public float trainTimer;

    #region Singleton Logic
    private static MachineManager instance;
    public static MachineManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("The MachineManager instance is NULL");
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

    void Start()
    {
        peças.AddRange(FindObjectsOfType<PecaScript>());
        pecasFonte.AddRange(FindObjectsOfType<PecaFonteScript>());
    }

    #region FirstMachine
    public bool AreAllPiecesCorrectlyPlaced()
    {
        bool allCorrect = peças.All(p => p.isRight == true);

        if (allCorrect == true && SourcePieceName() == "Carvão")
        {
            for (int i = 0; i < peças.Count; i++)
            {
                Animator anim = peças[i].gameObject.GetComponent<Animator>();

                if(anim != null)
                {
                    anim.SetTrigger("Play");
                    anim.speed = 1.0f;
                }
            }

            for (int i = 0; i < adicionalAnimators.Count; i++)
            {
                adicionalAnimators[i].SetTrigger("Play");
                adicionalAnimators[i].speed = 1.0f;
            }

            Time.timeScale = 0.0f;
        }

        return allCorrect;
    }

    public void StopMachine()
    {
        for (int i = 0; i < peças.Count; i++)
        {
            Animator anim = peças[i].gameObject.GetComponent<Animator>();

            if (anim != null)
            {
                anim.speed = 0.0f;
            }
        }

        for (int i = 0; i < adicionalAnimators.Count; i++)
        {
            adicionalAnimators[i].speed = 0.0f;
        }

        machineIsFunctioning = false;
    }

    public bool AreAllPiecesGlued()
    {
        return peças.All(p => p.colado == true);
    }

    public bool IsThereASourcePieceGlued()
    {
        return pecasFonte.Any(p => p.isGlued == true);
    }

    public string SourcePieceName()
    {
        string name = "";

        if (IsThereASourcePieceGlued())
        {
            name = pecasFonte.FirstOrDefault(p => p.isGlued).name;
        }

        return name;
    }
    #endregion

    #region Train
    public void PlayTrainAnimation()
    {
        GetComponent<Animator>().SetBool("isFunctioning", true);
        transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
        UIManager.Instance.canUpdatePathSlider = true;
    }

    public void StopTrainAnimation()
    {
        GetComponent<Animator>().SetBool("isFunctioning", false);
        transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        UIManager.Instance.canUpdatePathSlider = false;
    }
    #endregion
}
