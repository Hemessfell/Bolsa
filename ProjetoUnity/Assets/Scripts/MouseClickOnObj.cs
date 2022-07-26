
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseClickOnObj : MonoBehaviour
{
    //TEXTOS EXIBIDOS
    public TextMeshProUGUI texto_trabalho, texto_Qq, texto_Qf, texto_N;

    //VARIAVEIS ALTERAVEIS

    private int trabalho;
    private int qqcalor;
    private int qfcalor;
    private int rendimento;
    

    //MOUSE
    [SerializeField] private Transform obj;

    private bool _drag;

    private Vector2 _mouse;

    private RaycastHit2D hit;

    private Vector2 _offset;

    [SerializeField] private LayerMask layer;

    [SerializeField] private Texture2D cursorTexture;

    private Vector2 cursorHotspot;


    // Start is called before the first frame update
    void Start()
    {
        //DEFININDO O CALOR, RENDIMENTO E TRABALHO
        trabalho = 12;
        rendimento = 20;
        qfcalor = 48;
        qqcalor = 60;
      
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height /2);
        Cursor.SetCursor(cursorTexture, cursorHotspot,CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    { 
        texto_trabalho.text = "W: " + trabalho + "j";
        texto_Qq.text = "Qq: " + qqcalor + "j";
        texto_Qf.text = "Qf: " + qfcalor + "j";
        texto_N.text = "N: " + rendimento + "%";

        _mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(_mouse,Vector2.zero,0f, layer);

        if(UIManager.Instance.canClickOnPieces)
            follow();
    }

    void follow()
    {
        if(Input.GetMouseButtonDown(0) && hit && !_drag){
            _drag = true;

            obj = hit.transform;

            float _x = obj.transform.position.x - _mouse.x;
            float _y = obj.transform.position.y - _mouse.y;

            _offset = new Vector2(_x, _y);
        }

        if(_drag){
            obj.transform.position = _mouse + _offset;
        }

        if(Input.GetMouseButtonUp(0) && _drag){
            obj = null;
            _drag = false;
            _offset = new Vector2(0f, 0f);
        }
    }
}
