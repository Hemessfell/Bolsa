using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoPlayScript : MonoBehaviour
{

    private bool tudookay;



    // Start is called before the first frame update
    void Start()
    {
        tudookay = false;
    }

    // Update is called once per frame
    void Update()
    {
        //print("Est�: " + tudookay);


        if(tudookay == true){
            //print("Est�: Funcionando");
        }
        else
        {
            //print("Est�: N�o Funcionando");
        }

    }
}
