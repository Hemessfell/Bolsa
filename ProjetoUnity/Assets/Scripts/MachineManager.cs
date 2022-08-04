using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    private List<PecaScript> peças = new List<PecaScript>();
    private List<PecaFonteScript> pecasFonte = new List<PecaFonteScript>();

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
        peças.AddRange(FindObjectsOfType<PecaScript>());
        pecasFonte.AddRange(FindObjectsOfType<PecaFonteScript>());
    }

    public bool AreAllPiecesCorrectlyPlaced()
    {
        bool allCorrect = new bool();

        for (int i = 0; i < peças.Count; i++)
        {
            if (peças[i].isRight == false)
            {
                allCorrect = false;
                break;
            }
            else
                allCorrect = true;
        }

        return allCorrect;
    }

    public bool AreAllPiecesGlued()
    {
        bool allGlued = new bool();

        for (int i = 0; i < peças.Count; i++)
        {
            if (peças[i].colado == false)
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
