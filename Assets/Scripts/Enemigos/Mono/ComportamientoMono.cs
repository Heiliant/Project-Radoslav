using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoMono : MonoBehaviour
{
    private GameObject player;
    public GameObject poder;

    private bool D;
    private bool C;
    private bool I;

    private GameObject torso;
    public GameObject PALADGO;
    public GameObject PALAIGO;
    private EdgeCollider2D palaD;
    private EdgeCollider2D palaI;

    private Animator animacionMono;

    private GameObject paredD;
    private GameObject paredI;

    public float monoHP = 3f;
    public float tripaHP = 10f;
    public float stunTime = 10f;

    private float tripavida;
    private float stunsave;

    private bool dmgCont = true;
    public bool hasBeenDmgd = false;
    private bool stuneado = false;
    public float localcolorBody = 1;
    private float localcolorFace = 1;

    private bool check = false;

    private bool bossFight = false;

    public Transform powerSpawn;
    public GameObject poderinfo;
    public void startFight()
    {
        bossFight = true;
    }

    // Use this for initialization
    void Start()
    {
        GetComponent<Transform>().position = new Vector3(139.82f, 46.49f, 0);
        animacionMono = GetComponent<Animator>();
        
        player = GameObject.FindGameObjectWithTag("Player");

        torso = GameObject.FindGameObjectWithTag("Torso M");

        palaD = PALADGO.GetComponent<EdgeCollider2D>();
        palaI = PALAIGO.GetComponent<EdgeCollider2D>();

        paredD = GameObject.FindGameObjectWithTag("zonas1");
        paredI = GameObject.FindGameObjectWithTag("zonas2");

        paredD.SetActive(false);
        paredI.SetActive(false);

        tripavida = tripaHP;
        stunsave = stunTime;


        foreach (SpriteRenderer a in GetComponentsInChildren<SpriteRenderer>())
        {

            a.color = new Color(1, 1, 1, 0);
        }

        GetComponentInChildren<PlataformaAtravesable>().lifeTime = -1f;

    }




    private void FixedUpdate()
    {
        if (bossFight)
        {

            if (GetComponentInChildren<SpriteRenderer>().color.a < 1)
            {
                foreach (SpriteRenderer a in GetComponentsInChildren<SpriteRenderer>())
                {
                    float x = a.color.a;
                    x += 0.01f;
                    a.color = new Color(1, 1, 1, x);
                }
            }
        }
        if (GetComponentInChildren<SpriteRenderer>().color.a >= 1) {


            D = GameObject.FindObjectOfType<TriggerZonaD>().getD();
            C = GameObject.FindObjectOfType<TriggerZonaC>().getC();
            I = GameObject.FindObjectOfType<TriggerZonaI>().getI();


            //He cambiado la sintaxis zonaC.GetComponent<TriggerZonaC>().CisTriggered) por zonaC.CisTriggered
            //ya que ahora, zonaC (y las otras) no son gameobjects, son el script tal cual.
            //ACTUALIZACIÓN DE LOS DATOS DEL ANIMATOR

            animacionMono.SetBool("derecha", D);
            animacionMono.SetBool("centro", C);
            animacionMono.SetBool("izquierda", I);
            animacionMono.SetFloat("Tripa HP", tripaHP);
            animacionMono.SetFloat("Mono HP", monoHP);
            animacionMono.SetFloat("Stun Time", stunTime);
            animacionMono.SetBool("hasBeenDmgd", hasBeenDmgd);


            if (D)
            {
                animacionMono.SetBool("derecha", true);
                animacionMono.SetBool("izquierda", false);
                animacionMono.SetBool("centro", false);
            }

            else if (I)
            {
                animacionMono.SetBool("derecha", false);
                animacionMono.SetBool("izquierda", true);
                animacionMono.SetBool("centro", false);
            }

            else if (C)
            {
                animacionMono.SetBool("derecha", false);
                animacionMono.SetBool("izquierda", false);
                animacionMono.SetBool("centro", true);
            }

            if (monoHP == 0) {
                StartCoroutine(killMono(9.0f));
                GameObject.Find("plataformaTapa").GetComponentInChildren<BoxCollider2D>().enabled = false;
                    }
            //SI LA BARRIGA NO TIENE VIDA
            if (tripaHP == 0)
            {
                palaD.enabled = false;
                palaI.enabled = false;

                stuneado = true; //ENTRA EN MODO STUNT
                if (C)
                {   //SI ESTÁS EN LA ZONA DEL CENTRO, PUEDES COLISIONAR CON LAS PALAS Y HACER WALLJUMP, SINO NO
                    paredD.SetActive(true);
                    paredI.SetActive(true);

                    if (GameObject.FindGameObjectWithTag("Player").transform.position.y >
                        GetComponentInChildren<PlataformaAtravesable>().transform.position.y) //SI ESTÁS ENCIMA DE LA TAPA DEL CASCO Y PEGAS, ESTANDO EL MONO STUNEADO
                                                                                              //, QUITAS VIDA AL MONO Y LE DESACTIVAS EL STUNT Y RESETEAS SU VIDA DE TRIPA
                    {
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().puñetazo && stuneado)
                        {
                            monoHP--;
                            hasBeenDmgd = true;
                            stuneado = false;
                            tripaHP = tripavida;
                            stunTime = stunsave;
                            localcolorFace = 0;
                        }
                    }

                }

                else
                {
                    paredD.SetActive(false);
                    paredI.SetActive(false);
                }

                //CADA FOTOGRAMA QUE PASA, REDUCIMOS EL stuntTime, QUE ES EL TIEMPO QUE EL MONO ESTARÁ STUNEADO SI NO SE LE PEGA
                stunTime -= Time.deltaTime;

                if (stunTime <= 0)
                {
                    stuneado = false;
                    tripaHP = tripavida;
                    stunTime = stunsave;
                    hasBeenDmgd = false;
                }
            }

            //TRIPA SI TIENE VIDA
            else
            {
                StartCoroutine(ResetPalas(2));
                
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().puñetazo && dmgCont)
                {
                    if (tripaHP > 0)
                    {
                        //SI ESTÁS EN CENTRO, PEGAS UN PUÑETAZO, Y TRIPA TIENE VIDA, LE QUITAS VIDA A TRIPA
                        tripaHP--;
                        dmgCont = false; //Esta variable es para no quitarle vida cada fotograma que puñetazo está activo
                    }
                }
                if (!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().puñetazo)
                    dmgCont = true;

                paredD.SetActive(false);
                paredI.SetActive(false);

               // hasBeenDmgd = false;
            }

            //CAMBIA EL COLOR DE TRIPA EN FUNCIÓN DE SU VIDA
            localcolorBody = tripaHP / tripavida;
            torso.GetComponent<SpriteRenderer>().color = new Color(1f, localcolorBody, localcolorBody, 1f);

            

            float monolastHP = monoHP;
            if (hasBeenDmgd) {
                if (localcolorFace < 1)
                {
                    localcolorFace += Time.deltaTime;
                    GameObject.Find("CabezaM").GetComponent<SpriteRenderer>().color = new Color(1, localcolorFace, localcolorFace, 1);
                    check = true;
                }
                else
                {
                    localcolorFace = 0;
                    if (check)
                        hasBeenDmgd = false; check = false;
                }
            }
            
        }
    }
    IEnumerator killMono(float s)
    {
        yield return new WaitForSeconds(s);
        GameObject pow=Instantiate(poder, powerSpawn.position, new Quaternion(0, 0, 0, 1));
        pow.transform.position = new Vector3(pow.transform.position.x, pow.transform.position.y, -22f);
        pow.GetComponent<PoderSpawn>().relevant = poderinfo;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>().regularSize();
        GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().mono = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();
        Destroy(gameObject);
    }

    IEnumerator ResetPalas(float a)
    {
        yield return new WaitForSeconds(a);
        palaD.enabled = true;
        palaI.enabled = true;
    }
}

