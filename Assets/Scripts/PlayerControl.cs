using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    //mimebros de animacion
    public GameObject Humana;       //Aqui meto los gameobjects de Huama y Demonio. Se podria hacer a través de GetComponentInChildren, per creo que asi                            
    public GameObject Demonio;
    private bool demon = false;
    public Animator animacionHumana;

    //mimebros de movimiento simple
    public float FuerzaPasos = 10f;  
    public float FuerzaSalto = 20f;
    private float VelX;
    
    //miembros de movimiento elaborado
    public Transform DetectorSuelo;
    public float radDetSuelo = 0.40f;
    public bool enSuelo = false;
    public LayerMask suelo;

    public float radDetPared=1.25f;
    public bool enPared;
    public LayerMask pared;
    public float FuerzaWallJump;

    //métodos de acceso para scripts externos
    public bool getEnpared()
    {
        return enPared;
    }

    public float getFuerzawalljump()
    {
        return FuerzaWallJump;
    }

    //-----------------------------


    void Start () {
        Demonio.SetActive(false);
        Humana.SetActive(true);
    }

    private void FixedUpdate()
    {
        Demonio.SetActive(demon); //Regulo el que gameobject está activo, si Demonio o Humana.
        Humana.SetActive(!demon);

        VelX = GetComponent<Rigidbody2D>().velocity.x;
        animacionHumana.SetFloat("VelX", VelX);

        enSuelo = Physics2D.OverlapCircle(DetectorSuelo.position, radDetSuelo, suelo);
        enPared = Physics2D.OverlapCircle(GetComponent<Transform>().position, radDetPared, pared);
    }
    // Update is called once per frame
    void Update () {


        //movimiento simple

        if (Input.GetKey(KeyCode.A))
        {
            if(enSuelo)
                //cambiar vector de velocidad "a saco"
                GetComponent<Rigidbody2D>().velocity = new Vector2(-FuerzaPasos, GetComponent<Rigidbody2D>().velocity.y);
            else
                //aplicar fuerza gradualmente
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-FuerzaPasos, GetComponent<Rigidbody2D>().velocity.y));
        }

        else if (Input.GetKey(KeyCode.D))
        {
            if (enSuelo)
                //cambiar vector de velocidad "a saco"
                GetComponent<Rigidbody2D>().velocity = new Vector2(FuerzaPasos, GetComponent<Rigidbody2D>().velocity.y);
            else
                //aplicar fuerza gradualmente
                GetComponent<Rigidbody2D>().AddForce(new Vector2(FuerzaPasos, GetComponent<Rigidbody2D>().velocity.y));
        }

        //movimiento elaborado

        if (Input.GetKeyDown(KeyCode.Space) && enSuelo && !enPared)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, FuerzaSalto);
            enSuelo = false;
        }

        //transformación

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (demon)
                demon = false;
            else
                demon = true;
        }


    }
}
