using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncaixeScript : MonoBehaviour
{ 

    public string objetocerto;
    public bool cheio;
    private GameObject objetoencaixado;
    private bool podeencaixar;


    // Start is called before the first frame update
    void Start()
    {
       cheio = false;
        podeencaixar = true;
    }

    // Update is called once per frame
    void Update()
    {
        


        print(objetoencaixado);
    
        
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        objetoencaixado = collider.gameObject;
    }
}
