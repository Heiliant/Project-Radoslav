using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallJump : MonoBehaviour
{
    public GameObject prota;
    public PlayerControl script;

    private float FuerzaYOriginal = 1;
    public float fuerzaY;//
    public float incrementoCaida = 1;
    public float contador = 0;//
    public float segToFall = 1;
    public float fuerzaWallJump=900;

    public bool derecha;
    public int sentido;//

    // Use this for initialization
    void Start()
    {
        prota = GameObject.FindGameObjectWithTag("Player");

        script = prota.GetComponent<PlayerControl>();

        fuerzaY = FuerzaYOriginal;

        if (derecha)
            sentido = 1;
        else
            sentido = -1;
    }

    private void FixedUpdate()
    {

        if (!script.getEnpared())
        {
            contador = 0;
            fuerzaY = FuerzaYOriginal;
        }
    }


    void Update()
    {


    }

    private void OnCollisionStay2D(Collision2D collision)

    {

        if (collision.gameObject.tag == "Player")
        {

            contador += Time.deltaTime;
            if (contador > segToFall)
            {
                fuerzaY -= (Time.deltaTime * incrementoCaida);
            }


            if ((Input.GetKey(KeyCode.D) && (!derecha)) || ((derecha) && Input.GetKey(KeyCode.A)))

            {
                prota.GetComponent<Rigidbody2D>().velocity = new Vector2(0, fuerzaY);


                if (Input.GetKey(KeyCode.Space) && script.getEnpared()) //*PROLEMILLA* Si saltas hacia la pared con el espacio 
                    //pulsado, rebotas directamente. He probado con un GetKeyDown para hacer que saltase solo en pulsar espacio
                    //pero el compotamiento es inesperado.
                {
                    prota.GetComponent<Rigidbody2D>().AddForce(new Vector2(fuerzaWallJump * sentido, fuerzaWallJump));
                    //prota.GetComponent<Rigidbody2D>().velocity = new Vector2(script.getFuerzawalljump()*sentido*2, script.getFuerzawalljump()*2f);
                }

            }
        }

    }
}
