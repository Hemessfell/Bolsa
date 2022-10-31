using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeButton : MonoBehaviour
{
    public bool eyeIsOpen;
    [SerializeField] private Sprite open, close;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        eyeIsOpen = false;
        button.GetComponent<Image>().sprite = close;
    }

    public void OpenEye(int index)
    {
        if (eyeIsOpen)
        {
            if(index == 0)
                UIManager.Instance.texto_trabalho.text = "W: --------";
            if (index == 1)
                UIManager.Instance.texto_Qq.text = "Qq: --------";
            if (index == 2)
                UIManager.Instance.texto_Qf.text = "Qf: --------";
            if (index == 3)
                UIManager.Instance.texto_N.text = "N: --------";

            button.GetComponent<Image>().sprite = close;
            eyeIsOpen = false;
        }
        else
        {
            if(index == 0)
                UIManager.Instance.texto_trabalho.text = "W: " + UIManager.Instance.trabalhoTxt;
            if (index == 1)
                UIManager.Instance.texto_Qq.text = "Qq: " + UIManager.Instance.QqTxt;
            if (index == 2)
                UIManager.Instance.texto_Qf.text = "Qf: " + UIManager.Instance.QfTxt;
            if (index == 3)
                UIManager.Instance.texto_N.text = "N: " + UIManager.Instance.NTxt;

            button.GetComponent<Image>().sprite = open;
            eyeIsOpen = true;
        }
    }
}
