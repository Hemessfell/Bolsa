using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PecaScript : MonoBehaviour
{

    public GameObject peca, myCamera;

    private GameObject encaixe;

    public string nome;

    private bool tocou;
    public bool isRight, colado;

    public float peca_x, peca_y, peca_z;

    // Start is called before the first frame update
    void Start()
    {
        tocou = false;
        colado = false;
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
            if(tocou == true)
            {
                this.gameObject.transform.position = new Vector3(peca_x, peca_y, peca_z);
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
        //print(collider.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 && colado)
        {
            colado = false;
            encaixe.GetComponent<EncaixeScript>().cheio = false;
            isRight = false;
        }
    }
}