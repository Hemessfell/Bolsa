using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PecaFonteScript : MonoBehaviour
{

    public GameObject peca;

    private bool tocou;

    public bool isGlued;

    public GameObject camera;

    public float peca_x;

    public float peca_y;

    public float peca_z;

    // Start is called before the first frame update
    void Start()
    {
        tocou = false;


    }

    // Update is called once per frame
    void Update()
    {
        //print(tocou);

        if (Input.GetMouseButtonDown(0))
        {
            tocou = false;
        }
        else
        {

            if (tocou == true)
            {
                this.gameObject.transform.position = new Vector3(peca_x, peca_y, peca_z);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Encaixe") && collider.gameObject.GetComponent<EncaixeScript>().myStates == EncaixeScript.States.peçaFonte)
        {
            peca_x = collider.gameObject.transform.position.x;
            peca_y = collider.gameObject.transform.position.y;
            peca_z = collider.gameObject.transform.position.z;

            gameObject.transform.position = new Vector3(peca_x, peca_y, peca_z);
            tocou = true;
            isGlued = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 7 && isGlued)
        {
            isGlued = false;
        }
    }
}

