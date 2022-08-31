using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    private List<PecaScript> pe�as = new List<PecaScript>();
    private List<PecaFonteScript> pecasFonte = new List<PecaFonteScript>();

    public List<Animator> adicionalAnimators;

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
        }else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    void Start()
    {
        pe�as.AddRange(FindObjectsOfType<PecaScript>());
        pecasFonte.AddRange(FindObjectsOfType<PecaFonteScript>());
    }

    public bool AreAllPiecesCorrectlyPlaced()
    {
        bool allCorrect = new bool();

        for (int i = 0; i < pe�as.Count; i++)
        {
            if (pe�as[i].isRight == false)
            {
                allCorrect = false;
                break;
            }
            else
                allCorrect = true;
        }

        if (allCorrect == true && SourcePieceName() == "Carv�o")
        {
            for (int i = 0; i < pe�as.Count; i++)
            {
                Animator anim = pe�as[i].gameObject.GetComponent<Animator>();

                if(anim != null)
                {
                    anim.SetTrigger("Play");
                }
            }

            for (int i = 0; i < adicionalAnimators.Count; i++)
            {
                adicionalAnimators[i].SetTrigger("Play");
            }

            Time.timeScale = 0.0f;
        }

        return allCorrect;
    }

    public bool AreAllPiecesGlued()
    {
        bool allGlued = new bool();

        for (int i = 0; i < pe�as.Count; i++)
        {
            if (pe�as[i].colado == false)
            {
                allGlued = false;
                break;
            }
            else
                allGlued = true;
        }

        return allGlued;
    }

    public bool IsThereASourcePieceGlued()
    {
        bool isGlued = new bool();

        for (int i = 0; i < pecasFonte.Count; i++)
        {
            if (pecasFonte[i].isGlued)
            {
                isGlued = true;
                break;
            }
            else
                isGlued = false;
        }

        return isGlued;
    }

    public string SourcePieceName()
    {
        string name = "";

        if (IsThereASourcePieceGlued())
        {
            for (int i = 0; i < pecasFonte.Count; i++)
            {
                if (pecasFonte[i].isGlued)
                {
                    name = pecasFonte[i].name;
                    break;
                }
                else
                    name = "";
            }
        }

        return name;
    }
}
