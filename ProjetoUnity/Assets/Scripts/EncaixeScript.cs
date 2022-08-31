using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncaixeScript : MonoBehaviour
{ 

    public string objetocerto;
    public bool cheio;
    public GameObject objetoencaixado;

    private SpriteRenderer spr;

    public enum States { peçaNormal, peçaFonte};
    public States myStates;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
       cheio = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == objetoencaixado)
        {
            cheio = false;
            objetoencaixado = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(objetoencaixado != null)
        {
            cheio = true;
        }
        else
        {
            cheio = false;
        }
    }

    public void SetSpriteAndQuestionMark(bool state)
    {
        spr.enabled = state;
        transform.GetChild(0).gameObject.SetActive(state);
    }
}
