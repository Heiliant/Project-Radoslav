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
    // public float radius;
    public float DistanciaAtaque;


    //para el patron del movimiento
    public Transform destino;
    public Transform destino2;
    private Transform[] array = new Transform[2];
    Vector2 VectorMovimiento;
    private float module;
    public float speedxOrigin;
    private float speedx;
    private bool controlRotacion;
    private float PasadaX;
    Vector2 VectorAuxiliar1;
    Vector2 VectorAuxiliar2;
    public float hp = 1;
    public float lasthp;
    private Animator animescualo;
    public LayerMask suelo;
    public bool damaged=false;
    public float counter=0;

    public void harm(bool a)//true derecha false izquierda
    {
        int localA = a ? 1 : -1;
        hp--;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(9900*localA, 2000));
    }

    public void harmWeak()
    {
        hp-=0.05f/3;
    }

    public void knockback(bool a)
    {
        int localA = a ? 1 : -1;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(9900 * localA, 2000));
        speedxOrigin+=0.2f;
    }

    // Use this for initialization
    void Start()
    {
        animescualo = GetComponent<Animator>();
        //para el patron de movimiento

        array[0] = destino;
        array[1] = destino2;
        VectorMovimiento = (array[0].position - transform.position);
        controlRotacion = false;

        //para que te detecte
        detectado = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //player tomara el valor de los transform del game object que tenga el script Personajeprincipal

        lasthp = hp;
    }

    private void FixedUpdate()
    {
        if (detectado)
            speedx = 0.5f;
        else
            speedx = speedxOrigin;
        if (modulo < DistanciaAtaque)
            speedx = 0f;

        

        animescualo.SetBool("enRango", modulo<DistanciaAtaque);
        animescualo.SetFloat("VelX", speedx);
        animescualo.SetFloat("HP", hp);
        animescualo.SetBool("damaged", damaged);

        AnimatorStateInfo ScualoState = animescualo.GetCurrentAnimatorStateInfo(0);
        if (ScualoState.IsName("attack"))
        {
            speedx = 0;
        }
        if (ScualoState.IsName("death"))
        {
            speedx = 0;
            StartCoroutine(Autodestroy(1.55f));
        }
    }

    void Update()
    {


        move = (player.position - transform.position); //move vale la distancia entre player y el enemigo
        multiplicacionElementos = (move.x * move.x)+(move.y*move.y);//soloe staba teniendo en cuenta las x. He añadido las Y.
        modulo = Mathf.Sqrt(multiplicacionElementos); //modulo de la distáncia para no tener en cuenta el símbolo

        if (detectado)
        {
            if (modulo < DistanciaAtaque)
            {
                move = new Vector2(0, 0);
            }
            else
            {
                if (player.position.x == transform.position.x)
                {
                    transform.Rotate(0, 0, 0);
                }

                if (modulo < DistanciaAtaque)
                {
                    move = new Vector2(0, 0);
                    transform.Translate(move);
                }
                if (player.position.x < transform.position.x && PosFinal.position.x > transform.position.x)
                //el prota esta a la izquiersa del enemigo y este le da la espalda
                {
                    transform.Rotate(0, 180, 0);
                    controlRotacion = false;
                }

                if (player.position.x < transform.position.x && PosFinal.position.x < transform.position.x)
                //el prota esta a la izquiersa del enemigo y este le esta mirando
                {
                    move = (player.position - transform.position);
                    move = new Vector2(move.x, 0.0f);

                    transform.Rotate(0, 0, 0);
                    transform.Translate(move * speedx * Time.deltaTime);
                }

                if (player.position.x > transform.position.x && PosFinal.position.x < transform.position.x)
                //el prota esta a la derecha del enemigo y este le da la espalda
                {
                    transform.Rotate(0, 180, 0);
                    controlRotacion = true;
                }

                if (player.position.x > transform.position.x && PosFinal.position.x > transform.position.x)
                //el prota esta a la derecha del enemigo y este le esta mirando
                {
                    move = (transform.position - player.position);
                    move = new Vector2(move.x, 0.0f);

                    transform.Rotate(0, 0, 0);
                    transform.Translate(move * speedx * Time.deltaTime);

                }

            }



        }
        else
        {
            if (!detectado && transform.position.x > array[1].position.x)
            {
                if (controlRotacion == true)
                {
                    transform.Rotate(0, 180, 0);
                    controlRotacion = false;
                    VectorMovimiento = (array[0].position - transform.position);
                    VectorAuxiliar1 = VectorMovimiento;
                }

            }
            if (!detectado && transform.position.x < array[0].position.x)
            {
                if (!controlRotacion)
                {
                    transform.Rotate(0, 180, 0);
                    controlRotacion = true;
                    VectorMovimiento = (transform.position - array[1].position);
                    VectorAuxiliar2 = VectorMovimiento;
                }
            }

            module = Mathf.Sqrt(VectorMovimiento.x * VectorMovimiento.x);
            if (module >= 1.0f)
            {
                transform.Translate(VectorMovimiento * speedx * Time.deltaTime);
            }
            
        }

        if (!damaged)
            damaged = (lasthp != hp);
        else
        {
            counter += Time.deltaTime;
            speedx = 0;
            if (counter > 0.4f)
            {
                damaged = false;
                foreach (SpriteRenderer a in GetComponentsInChildren<SpriteRenderer>())
                {
                    a.color = new Color(1, 1, 1, 1);
                }
                speedx = speedxOrigin;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                counter = 0;
            }
        }

        lasthp = hp;
    }


    void OnTriggerEnter2D(Collider2D objeto)
    {
        if (objeto.GetComponent<Collider2D>().tag.Equals("humana") || objeto.GetComponent<Collider2D>().tag.Equals("demonio"))
        {
            detectado = true;
        }
    }

    void OnTriggerExit2D(Collider2D objeto)
    {
        if (objeto.GetComponent<Collider2D>().tag.Equals("humana") || objeto.GetComponent<Collider2D>().tag.Equals("demonio"))
        {
            detectado = false;
        }
    }

    IEnumerator Autodestroy(float a)
    {
        yield return new WaitForSeconds(a);
        Destroy(gameObject);
    }
}
