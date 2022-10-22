using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PecaScript : MonoBehaviour
{

    public GameObject peca, myCamera;

    public GameObject encaixe;

    private PecaScript sceneCoal;

    public string nome;

    private bool tocou, isQuitting;
    public bool isRight, colado, willDestroy, isBeingDragged;

    public float peca_x, peca_y, peca_z;

    // Start is called before the first frame update
    void Start()
    {
        tocou = false;
        colado = false;
    }

    private void OnEnable()
    {
        Application.quitting += SetApplicationQuitting;
    }

    // Update is called once per frame
    void Update()
    {
       //print(tocou);

        if (Input.GetMouseButtonDown(0)){
            tocou = false;
        }
        else
        {
            if(tocou)
            {
                gameObject.transform.position = new Vector3(peca_x, peca_y, peca_z);
                if (Input.GetMouseButtonUp(0) && willDestroy)
                    Destroy(gameObject);
            }
            else
            {
                if (Input.GetMouseButtonUp(0) && willDestroy && isBeingDragged)
                {
                    UIManager.Instance.UpdateCoalsCounter(true);
                    Destroy(gameObject);
                }
            }
        }

        if(isBeingDragged && nome == "coal")
        {
            if(sceneCoal == null)
            {
                PecaScript[] sceneCoals = FindObjectsOfType<PecaScript>();

                for (int i = 0; i < sceneCoals.Length; i++)
                {
                    if(sceneCoals[i] != this)
                    {
                        sceneCoal = sceneCoals[i];
                    }
                }
            }

            sceneCoal.isBeingDragged = false;
        }
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            if (tocou)
            {
                if(nome == "coal")
                {
                    CoalOBJ coal = FindObjectOfType<CoalOBJ>();

                    if (coal != null)
                    {
                        coal.UpdateQuantity(true);
                    }
                    else
                    {
                        GetComponent<Coal>().InstantiateCoal();
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Encaixe") && collider.gameObject.GetComponent<EncaixeScript>().myStates == EncaixeScript.States.peçaNormal)
        {
            encaixe = collider.gameObject;

            var nome_encaixe = encaixe.GetComponent<EncaixeScript>().objetocerto;
            var cheio_encaixe = encaixe.GetComponent<EncaixeScript>().cheio;

            if (!cheio_encaixe)
            {
                peca_x = collider.gameObject.transform.position.x;
                peca_y = collider.gameObject.transform.position.y;
                peca_z = collider.gameObject.transform.position.z;

                gameObject.transform.position = new Vector3(peca_x, peca_y, peca_z);
                tocou = true;
                cheio_encaixe = true;
                encaixe.GetComponent<EncaixeScript>().cheio = cheio_encaixe;
                colado = true;
                encaixe.GetComponent<EncaixeScript>().objetoencaixado = gameObject;
                encaixe.GetComponent<EncaixeScript>().SetSpriteAndQuestionMark(false);

                if (nome == nome_encaixe)
                {
                    isRight = true;
                }
                else
                {
                    isRight = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 && colado)
        {
            colado = false;
            encaixe.GetComponent<EncaixeScript>().cheio = false;
            if(nome != "coal")
                encaixe.GetComponent<EncaixeScript>().SetSpriteAndQuestionMark(true);
            isRight = false;
        }
    }

    private void SetApplicationQuitting()
    {
        isQuitting = true;
    }
}