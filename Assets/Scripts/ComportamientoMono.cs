using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoMono : MonoBehaviour
{
    private GameObject player;

    private bool D;
    private bool C;
    private bool I;

    public GameObject torso;
    private EdgeCollider2D palaD;
    private EdgeCollider2D palaI;

    private Animator animacionMono;

    private GameObject paredD;
    private GameObject paredI;

    public float monoHP = 3f;
    public float tripaHP = 10f;
    public float stunTime = 5f;

    private float tripavida;
    private float stunsave;

    private bool dmgCont = true;
    private bool hasBeenDmgd = false;
    private bool stuneado = false;
    public float localcolor = 1;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        palaD = GameObject.FindGameObjectWithTag("palad").GetComponent<EdgeCollider2D>();
        palaI = GameObject.FindGameObjectWithTag("palai").GetComponent<EdgeCollider2D>();

        paredD = GameObject.FindGameObjectWithTag("zonas1");
        paredI = GameObject.FindGameObjectWithTag("zonas2");

        paredD.SetActive(false);
        paredI.SetActive(false);

        animacionMono = GetComponent<Animator>();

        tripavida = tripaHP;
        stunsave = stunTime;
    }


    private void FixedUpdate()
    {

        if (GameObject.Find("ZonaD").GetComponent<TriggerZonaD>().getD())
        {
            Debug.Log("funciono");
        }
        else
        {
            Debug.Log("no funciono");
        }

        if (GameObject.Find("ZonaD").GetComponent<TriggerZonaD>().getD())
        {
            D = true;
        }
        else
        {
            D = false;
        }

        if (GameObject.Find("ZonaC").GetComponent<TriggerZonaC>().getC())
        {
            C = true;
        }
        else
        {
            C = false;
        }

        if (GameObject.Find("ZonaI").GetComponent<TriggerZonaI>().getI())
        {
            I = true;
        }
        else
        {
            I = false;
        }

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
            palaD.enabled = true;
            palaI.enabled = true;
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

            hasBeenDmgd = false;
        }

        //CAMBIA EL COLOR DE TRIPA EN FUNCIÓN DE SU VIDA
        localcolor = tripaHP / tripavida;
        
        torso.GetComponent<SpriteRenderer>().color = new Color(1f, localcolor, localcolor, 1f);
        
    }
}

