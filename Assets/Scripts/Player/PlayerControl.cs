using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private Transform initialTorsoSave;

    //mimebros de animacion
    public Animator animacionHumana;

    //mimebros de movimiento simple
    public float FuerzaPasos = 1100f;  
    public float FuerzaSalto = 20f;
    private float VelX;
    
    //miembros de movimiento elaborado
    public bool enSuelo = false;

    public float radDetPared=1.25f;
    public bool enPared;
    public LayerMask pared;

    public Transform DetectorTecho;


    public bool puñetazo = false;
    private float segundero=0;



    private bool inmovil = false;

    private KeyCode JUMP = KeyCode.Space;
    private KeyCode LEFT = KeyCode.A;
    private KeyCode RIGHT = KeyCode.D;
    private KeyCode DOWN = KeyCode.S;
    private KeyCode PUÑO = KeyCode.Mouse0;

    public int amountOfJumps = 0;
    public int jumpCount;
 

    //métodos de acceso para scripts externos
    public bool getEnpared()
    {
        return enPared;
    }

    public Transform getTorsoSave()
    {
        return initialTorsoSave;
    }

    public float getVelX()
    {
        return GetComponent<Rigidbody2D>().velocity.x;
    }

    public void stayQuiet(bool a)
    {
        inmovil = a;
    }

    public void setAmountOfJumps(int a)
    {
        amountOfJumps = a;
    }
    public int getAmountOfJumps()
    {
        return amountOfJumps;
    }
    public void setJumpKey(KeyCode a)
    {
        JUMP = a;
    }
    public void setLeftKey(KeyCode a)
    {
        LEFT = a;
    }
    public void setRightKey(KeyCode a)
    {
        RIGHT = a;
    }
    public void setFistKey(KeyCode a)
    {
        PUÑO = a;

    }
    public void setDownKey(KeyCode a)
    {
        DOWN = a;
    }

    public KeyCode getLeft()
    {
        return LEFT;
    }
    public KeyCode getRight()
    {
        return RIGHT;
    }
    public KeyCode getJump()
    {
        return JUMP;
    }
    public KeyCode getFist()
    {
        return PUÑO;
    }

    //-----------------------------


    void Start () {
        initialTorsoSave = GameObject.FindGameObjectWithTag("muñeco").GetComponent<Transform>();
        Debug.Log(initialTorsoSave.rotation);
        Debug.Log("Human start");


        puñetazo = false;
 
        animacionHumana = GameObject.FindGameObjectWithTag("humana").GetComponent<Animator>();

        jumpCount = amountOfJumps;
    }

    private void FixedUpdate()
    {
            VelX = GetComponent<Rigidbody2D>().velocity.x;
        
            animacionHumana.SetFloat("VelX", VelX);
            animacionHumana.SetBool("enSuelo 0", enSuelo);
            animacionHumana.SetBool("enPared", enPared);
            animacionHumana.SetBool("puñetazo", puñetazo);
        

            enSuelo = FindObjectOfType<CambioFormas>().enSuelo;
            enPared = Physics2D.OverlapCircle(GetComponent<Transform>().position, radDetPared, pared);


        if(VelX > 0.1f && !enPared)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 1f);
        }
        else if(VelX < -0.1f || enPared)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 1f);

        if (enSuelo)
        {
            jumpCount = amountOfJumps;
        }

    }
    // Update is called once per frame
    void Update () {
        if (!inmovil)
        {
            if (Input.GetKey(KeyCode.Z))
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1.70f), ForceMode2D.Impulse);
            //movimiento simple

            if (Input.GetKey(LEFT))
            {
                if (enSuelo)
                    //cambiar vector de velocidad "a saco"
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-FuerzaPasos, GetComponent<Rigidbody2D>().velocity.y);
                else
                    //aplicar fuerza gradualmente
                    if (GetComponent<Rigidbody2D>().velocity.x < -FuerzaPasos)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-FuerzaPasos, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-FuerzaPasos, 0));
            }

            else if (Input.GetKey(RIGHT))
            {
                if (enSuelo)
                    //cambiar vector de velocidad "a saco"
                    GetComponent<Rigidbody2D>().velocity = new Vector2(FuerzaPasos, GetComponent<Rigidbody2D>().velocity.y);
                else
                    //aplicar fuerza gradualmente
                    if (GetComponent<Rigidbody2D>().velocity.x > FuerzaPasos)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(FuerzaPasos, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(FuerzaPasos, 0)); //Si has saltado sin VelX apenas te 
                                                                                       // puedes acelerar en las X en el aire. Podemos cambiarlo multiplicando FuerzaPasos aqui si queremos
                                                                                       // que el PJ pueda acelerarse en el aire de la "nada"
            }

            else
            {
                if (GetComponent<Rigidbody2D>().velocity.x > 0.1f)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-FuerzaPasos * 2, 0));
                }
                else if (GetComponent<Rigidbody2D>().velocity.x < -0.1f)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(FuerzaPasos * 2, 0));
                }
            }

            //movimiento elaborado

            if (Input.GetKeyDown(JUMP) && enSuelo)
            {
                //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, FuerzaSalto);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, FuerzaSalto));
            }

            else if (Input.GetKeyDown(JUMP) && !enSuelo && jumpCount>0 && !enPared)
            {
                if (Input.GetKey(RIGHT))
                {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(FuerzaSalto * 1.4f, 0));
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, FuerzaSalto / 40);
                }
                else if (Input.GetKey(LEFT))
                {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(-FuerzaSalto*1.4f, 0));
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, FuerzaSalto / 40);
                }
                else
                {
                        //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, FuerzaSalto));
                        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, FuerzaSalto/40);
                }
                --jumpCount;
            }

                //combate 

                if (Input.GetKeyDown(PUÑO) && enSuelo)
            {
                puñetazo = true;
            }

            if (puñetazo)
            {
                segundero += Time.deltaTime;
                if (segundero >= 0.7f)
                {
                    puñetazo = false;
                    segundero = 0;
                }
            }

        }

    }   

    
}
