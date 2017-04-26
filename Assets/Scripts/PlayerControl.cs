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
    public float FuerzaPasos = 1100f;  
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

    public Transform DetectorTecho;

    public int currentHP;
    private int lastHP;
    public bool puñetazo = false;
    private float segundero=0;
    public float segunderoI = 0;

    public float RecoveryTime;
    private bool invulnerable=false;

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
    
    
    //-----------------------------


    void Start () {

        currentHP = 3;
        lastHP = currentHP;

        puñetazo = false;

        Demonio.SetActive(false);
        Humana.SetActive(true);
        animacionHumana = GameObject.FindGameObjectWithTag("humana").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
     

        Demonio.SetActive(demon); //Regulo que gameobject está activo, si Demonio o Humana.
        Humana.SetActive(!demon);

        VelX = GetComponent<Rigidbody2D>().velocity.x;
        

        if (!demon)
        {
            animacionHumana.SetFloat("VelX", VelX);
            animacionHumana.SetBool("enSuelo 0", enSuelo);
            animacionHumana.SetBool("enPared", enPared);
            animacionHumana.SetBool("puñetazo", puñetazo);
        }

        enSuelo = Physics2D.OverlapCircle(DetectorSuelo.position, radDetSuelo, suelo);
        enPared = Physics2D.OverlapCircle(GetComponent<Transform>().position, radDetPared, pared);


        if(VelX > 0.1f && !enPared)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 1f);
        }
        else if(VelX < -0.1f || enPared)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 1f);
        

    }
    // Update is called once per frame
    void Update () {

        if (Input.GetKey(KeyCode.Z))
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1.70f), ForceMode2D.Impulse);
        //movimiento simple

        if (Input.GetKey(KeyCode.A))
        {
            if(enSuelo)
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

        else if (Input.GetKey(KeyCode.D))
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
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-FuerzaPasos*2, 0));
            }
            else if(GetComponent<Rigidbody2D>().velocity.x < -0.1f)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(FuerzaPasos*2, 0));
            }
        }

        //movimiento elaborado

        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, FuerzaSalto);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, FuerzaSalto));
        }

        //combate 

        if (Input.GetKeyDown(KeyCode.Mouse0) && enSuelo )
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

        //transformación

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (demon)
                demon = false;
            else
                demon = true;
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
