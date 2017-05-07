using UnityEngine;
using System.Collections;

public class ComportamientoEnemigo : MonoBehaviour

{
    //DESTINO DEBE ESTAR A LA IZQUIERDA DEL OBJETO Y DESTINO 2 A LA DERECHA. SINO LE SUDA LA VIDA.


    //para que te detecte
    public Transform player; //esta variable podra tomar el valor de cualquier transform (transform.position, etc)
    public bool detectado = false;
    Vector2 move;
    private float modulo;
    private float multiplicacionElementos;
    public Transform PosFinal;
    public float radius;
    private bool InRange;
    public LayerMask mask = 11;


    //para el patron del movimiento
    public Transform destino;
    public Transform destino2;
    private Transform[] array = new Transform[2];
    Vector2 VectorMovimiento;
    private float module;
    public float speedx;
    private bool controlRotacion;
    private float PasadaX;
    //int hp = 20;
    private Animator animescualo;
    public LayerMask suelo;
    // Use this for initialization
    void Start()
    {
        animescualo = GetComponent<Animator>();

        //para el patron de movimiento
        speedx = 0.25f;
        array[0] = destino;
        array[1] = destino2;
        VectorMovimiento = (array[0].position - transform.position);
        controlRotacion = false;
        InRange = false;
        //para que te detecte
        detectado = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        //player tomara el valor de los transform del game object que tenga el script Personajeprincipal

    }

    private void FixedUpdate()
    {
        animescualo.SetFloat("VelX", (GetComponent<Transform>().position.x-PasadaX)/Time.deltaTime);
        animescualo.SetBool("enRango", InRange);
        animescualo.SetBool("enSuelo", Physics2D.OverlapCircle(new Vector2(GetComponent<Transform>().position.x,
            GetComponent<Transform>().position.y), suelo));
    }

    void Update()
    {
        InRange=Physics2D.OverlapCircle(transform.position, radius, mask.value);

        move = (player.position - transform.position); //move vale la distancia entre player y el enemigo
        multiplicacionElementos = (move.x * move.x);
        modulo = Mathf.Sqrt(multiplicacionElementos); //modulo de la distáncia para no tener en cuenta el símbolo


        if (modulo <= 15.0f)
        {
            detectado = true;

        }

        if (modulo >= 16.0f) 
        {
            detectado = false; //detectado vuelve a ser false euna vez sale de la linea de vision

        }

        if (detectado == true)
        {
            if (player.position.x == transform.position.x) 
            {
                transform.Rotate(0, 0, 0);
            }

            if (player.position.y > transform.position.y)
            {
                detectado = false;
            }

            if (player.position.x < transform.position.x && PosFinal.position.x > transform.position.x)
            //el prota esta a la izquiersa del enemigo y este le da la espalda
            {
                transform.Rotate(0, 180, 0);
            }

            if (player.position.x < transform.position.x && PosFinal.position.x < transform.position.x)
            //el prota esta a la izquiersa del enemigo y este le esta mirando
            {
                if (InRange == true)
                {
                    move = new Vector2(0,0);
                }
                else
                {
                    move = (player.position - transform.position);
                    move = new Vector2(move.x, 0.0f);
                    transform.Rotate(0, 0, 0);
                    transform.Translate(move * Time.deltaTime);
                }
               
            }

            if (player.position.x > transform.position.x && PosFinal.position.x < transform.position.x)
            //el prota esta a la derecha del enemigo y este le da la espalda
            {
                transform.Rotate(0, 180, 0);

            }

            if (player.position.x > transform.position.x && PosFinal.position.x > transform.position.x)
            //el prota esta a la derecha del enemigo y este le esta mirando
            {
                if (InRange == true)
                {
                    move = new Vector2(0, 0);
                }
                else
                {
                    move = (transform.position - player.position);
                    move = new Vector2(move.x, 0.0f);
                    transform.Rotate(0, 0, 0);
                    transform.Translate( move * Time.deltaTime);
                }
               
            }
        }
        else
        {
           

            if (transform.position.x <= array[0].position.x)
            {
                if (controlRotacion == false)
                {
                    transform.Rotate(0, 180, 0);
                    controlRotacion = true;
                }

                VectorMovimiento = (transform.position - array[1].position);
            }

            if (transform.position.x >= array[1].position.x)
            {
                if (controlRotacion == true)
                {
                    transform.Rotate(0, 180, 0);
                    controlRotacion = false;
                }

                VectorMovimiento = (array[0].position - transform.position);
            }
            module = Mathf.Sqrt(VectorMovimiento.x * VectorMovimiento.x);
            if (module >= 1.0f)
            {
                transform.Translate(VectorMovimiento * speedx * Time.deltaTime);
            }
        }

        PasadaX = GetComponent<Transform>().position.x;
    }

   void OnTriggerEnter2D(Collider2D objeto)
    {
        if (objeto.GetComponent<Collider2D>().tag == "caida")
        {

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 7.5f), ForceMode2D.Impulse);
        }
    }

    /* void OnCollisionEnter(Collider objeto)
     {
         if (objeto.GetComponent<Collider2D>().tag == "arrojadiza")
         {
             hp -= 10;
         }
     }*/

}
