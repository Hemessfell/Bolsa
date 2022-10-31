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

            UIManager.Instance.trabalhoTxt = values[valuesIndex].values[0];
            UIManager.Instance.texto_trabalho.text = UIManager.Instance.texto_trabalho.transform.GetChild(0).GetComponent<EyeButton>().eyeIsOpen ? values[valuesIndex].values[0] : "W: --------";

            UIManager.Instance.QqTxt = values[valuesIndex].values[1];
            UIManager.Instance.texto_Qq.text = UIManager.Instance.texto_Qq.transform.GetChild(0).GetComponent<EyeButton>().eyeIsOpen ? values[valuesIndex].values[1] : "Qq: --------";

            UIManager.Instance.QfTxt = values[valuesIndex].values[2];
            UIManager.Instance.texto_Qf.text = UIManager.Instance.texto_Qf.transform.GetChild(0).GetComponent<EyeButton>().eyeIsOpen ? values[valuesIndex].values[2] : "Qf: --------";

            UIManager.Instance.NTxt = values[valuesIndex].values[3];
            UIManager.Instance.texto_N.text = UIManager.Instance.texto_N.transform.GetChild(0).GetComponent<EyeButton>().eyeIsOpen ? values[valuesIndex].values[3] : "N: --------";

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
            UIManager.Instance.text_Potencia.text = "<i>P</I>: --------";
            UIManager.Instance.texto_trabalho.text = "W: --------";
            UIManager.Instance.texto_Qq.text = "Qq: --------";
            UIManager.Instance.texto_Qf.text = "Qf: --------";
            UIManager.Instance.texto_N.text = "N: --------";

            UIManager.Instance.TurnTextIntoDots();

            if (MachineManager.Instance.machineIsFunctioning)
            {

                MachineManager.Instance.StopMachine();
            }
        }
    }

    [System.Serializable]
    public class Values
    {
        public string[] values; 
    }
}