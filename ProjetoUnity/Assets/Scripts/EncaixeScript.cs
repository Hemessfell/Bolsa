using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncaixeScript : MonoBehaviour
{ 

    public string objetocerto;
    public bool cheio;
    private GameObject objetoencaixado;
    public enum States { peçaNormal, peçaFonte};
    public States myStates;


    // Start is called before the first frame update
    void Start()
    {
       cheio = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        objetoencaixado = collider.gameObject;
    }
}
