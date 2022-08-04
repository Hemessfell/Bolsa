using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PecaFonteScript : MonoBehaviour
{
    public GameObject peca;

    private bool tocou;

    public bool isGlued;

    public float peca_x;

    public float peca_y;

    public float peca_z;

    [SerializeField] private Values[] values; 

    void Start()
    {
        tocou = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tocou = false;
        }
        else
        {
            if (tocou == true)
            {
                gameObject.transform.position = new Vector3(peca_x, peca_y, peca_z);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Encaixe") && collider.gameObject.GetComponent<EncaixeScript>().myStates == EncaixeScript.States.peçaFonte && !MachineManager.Instance.IsThereASourcePieceGlued())
        {
            peca_x = collider.gameObject.transform.position.x;
            peca_y = collider.gameObject.transform.position.y;
            peca_z = collider.gameObject.transform.position.z;

            int valuesIndex = Random.Range(0, values.Length);

            UIManager.Instance.texto_trabalho.text = values[valuesIndex].values[0];
            UIManager.Instance.texto_Qq.text = values[valuesIndex].values[1];
            UIManager.Instance.texto_Qf.text = values[valuesIndex].values[2];
            UIManager.Instance.texto_N.text = values[valuesIndex].values[3];

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
            UIManager.Instance.texto_trabalho.text = "W:";
            UIManager.Instance.texto_Qq.text = "Qq:";
            UIManager.Instance.texto_Qf.text = "Qf:";
            UIManager.Instance.texto_N.text = "N:";
        }
    }

    [System.Serializable]
    public class Values
    {
        public string[] values; 
    }
}