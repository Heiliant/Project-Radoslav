using UnityEngine;
using System.Collections;

public class ComportamientoEnemigo : MonoBehaviour

{
    //DESTINO DEBE ESTAR A LA IZQUIERDA DEL OBJETO Y DESTINO 2 A LA DERECHA. SINO LE SUDA LA VIDA.


    //para que te detecte
    public Transform player; //esta variable podra tomar el valor de cualquier transform (transform.position, etc)
    public bool detectado = false;
    Vector2 move;
    //Vector2 move2;
    private float modulo;
    private float multiplicacionElementos;
    //public Transform PosInicial;
    public Transform PosFinal;

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


    // Use this for initialization
    void Start()
    {

        //para el patron de movimiento
        speedx = 0.25f;
        array[0] = destino;
        array[1] = destino2;
        VectorMovimiento = (array[0].position - transform.position);
        controlRotacion = false;
        //para que te detecte
        detectado = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        //player tomara el valor de los transform del game object que tenga el script Personajeprincipal

    }

    void Update()
    {

        move = (player.position - transform.position); //move vale la distancia entre player y el enemigo
        multiplicacionElementos = (move.x * move.x) + (move.y * move.y);
        modulo = Mathf.Sqrt(multiplicacionElementos); //modulo de la dist�ncia para no tener en cuenta el s�mbolo


        if (modulo <= 15.0f)
        {
            detectado = true;
        }

        if (modulo >= 16.0f) //deberia ser >15.0f
        {
            detectado = false; //detectado vuelve a ser false euna vez sale de la linea de vision

        }

        if (detectado == true)
        {
            if (player.position.x == transform.position.x) //supongo que esto se hace para trigear el ataque
            {
                transform.Rotate(0, 0, 0);


            }

            if (player.position.x < transform.position.x && PosFinal.position.x > transform.position.x) 
                //el prota esta a la izquiersa del enemigo y este le da la espalda
            {

                transform.Rotate(0, 180, 0);
                //transform.Translate(move * Time.deltaTime);
            }

        

        if (player.position.x < transform.position.x && PosFinal.position.x < transform.position.x) 
                //el prota esta a la izquiersa del enemigo y este le esta mirando
        {
            move = (player.position - transform.position);
            transform.Rotate(0, 0, 0);
            transform.Translate(move * Time.deltaTime);
        }

        if (player.position.x > transform.position.x && PosFinal.position.x < transform.position.x) 
                //el prota esta a la derecha del enemigo y este le da la espalda
        {
            transform.Rotate(0, 180, 0);

        }

        if (player.position.x > transform.position.x && PosFinal.position.x > transform.position.x) 
                //el prota esta a la derecha del enemigo y este le esta mirando
        {
            move = (player.position - transform.position);
            transform.Rotate(0, 0, 0);
            transform.Translate(-move * Time.deltaTime);
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

    }

   void OnTriggerEnter2D(Collider2D objeto)
    {
        if (objeto.GetComponent<Collider2D>().tag == "caida")
        {

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5.0f), ForceMode2D.Impulse);
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
