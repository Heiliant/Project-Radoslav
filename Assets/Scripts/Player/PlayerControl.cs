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

    public int currentHP;
    private int lastHP;
    public bool puñetazo = false;
    private float segundero=0;
    public float segunderoI = 0;

    public float RecoveryTime;
    private bool invulnerable=false;
    private bool inmovil = false;

    private KeyCode JUMP = KeyCode.Space;
    private KeyCode LEFT = KeyCode.A;
    private KeyCode RIGHT = KeyCode.D;
    private KeyCode DOWN = KeyCode.S;
    private KeyCode PUÑO = KeyCode.Mouse0;

    private int amountOfJumps = 0;
    private int jumpCount;
 

    //métodos de acceso para scripts externos
    public bool getEnpared()
    {
        return enPared;
    }

    public void attackPlayer(float a)
    {
        if (!invulnerable)
        {
            currentHP--;
            foreach (SpriteRenderer w in GetComponentsInChildren<SpriteRenderer>())
            {
                w.color = new Color(1, 0, 0, 1);
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2(FuerzaSalto/2*a, FuerzaSalto/2));
        }
    }

    public void attackPlayer()
    {
        if (!invulnerable)
        {
            currentHP--;
            foreach (SpriteRenderer w in GetComponentsInChildren<SpriteRenderer>())
            {
                w.color = new Color(1, 0, 0, 1);
            }
        }
    }
    
    public void killPlayer()
    {
        currentHP = 0;
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
    //-----------------------------


    void Start () {
        initialTorsoSave = GameObject.FindGameObjectWithTag("muñeco").GetComponent<Transform>();
        Debug.Log(initialTorsoSave.rotation);
        Debug.Log("Human start");
        currentHP = 3;
        lastHP = currentHP;

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

            else if (Input.GetKeyDown(JUMP) && jumpCount == 1)
            {
                if (Input.GetKey(RIGHT))
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(FuerzaSalto*1.2f, FuerzaSalto));
                }
                else if (Input.GetKey(LEFT))
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-FuerzaSalto*1.2f, FuerzaSalto));
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, FuerzaSalto));
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

        if (lastHP != currentHP)
        {
            invulnerable = true;
        }
        else
        {
            foreach (SpriteRenderer w in GetComponentsInChildren<SpriteRenderer>())
            {
                w.color = new Color(1, 1, 1, 1);
            }
        }

        if (segunderoI >= RecoveryTime)
        {
            invulnerable = false; 
            segunderoI = 0;
        }

        if (invulnerable)
        {
            segunderoI += Time.deltaTime;

            foreach (SpriteRenderer a in GetComponentsInChildren<SpriteRenderer>())
            {
                float localAlpha = segunderoI % 1;
                a.color = new Color(1f, 1f, 1f, localAlpha);
            }
        }

        lastHP = currentHP;
    }   

    
}
