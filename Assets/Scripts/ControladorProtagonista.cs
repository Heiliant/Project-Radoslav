using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorProtagonista : MonoBehaviour {
    private float VelX;
    public Animator animacionHumana;
    public float FuerzaPasos = 10f;  //variables publicas para el movimiento y el salto. Por si lo queremos tocar dentro de unity directamente.
    public float FuerzaSalto = 20f;
    public GameObject Humana;       //Aqui meto los gameobjects de Huama y Demonio. Se podria hacer a través de GetComponentInChildren, per creo que asi                            
    public GameObject Demonio;       //se ve más claro.
    private bool demon=false;  //Demon regula que gameobject está activo, si Humana o Demonio. Empieza siendo falso y, por lo tanto, es Humana quine está on

    public LayerMask suelo;
    public LayerMask plat;//Esto sirve para que el PJ solo pueda saltar si está tocando el suelo. Suelo es la capa y detectorsuelo un objeto.
    public LayerMask player;
    public Transform DetectorSuelo;
    public bool enSuelo=false;

    public void setenSuelo(bool a)
    {
        enSuelo = a;
    }

    // Use this for initialization
    void Start () {

        animacionHumana = GetComponentInChildren<Animator>();
        Demonio.SetActive(false);
	}
	
    void FixedUpdate()
    {
 
        Demonio.SetActive(demon); //Regulo el que gameobject está activo, si Dmeonio o Humana.
        Humana.SetActive(!demon);
        
        VelX = GetComponent<Rigidbody2D>().velocity.x;
        animacionHumana.SetFloat("VelX", VelX);
        enSuelo=Physics2D.OverlapCircle(DetectorSuelo.position, 0.18f, suelo);  //Con OverlapCircle hago que ensuelo sea true si el objeto DetectorSuelo
                                                                    //está a 0.18 unidades de distancia o menos de un objeto de capa suelo. 
                                                                    // Los bloques de suelo tienen la capa suelo.
    }



	// Update is called once per frame
	void Update () {
        //Controles básicos para ir probando cosillas. Falta pulirlos.
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-FuerzaPasos, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(FuerzaPasos, GetComponent<Rigidbody2D>().velocity.y);
        }


        /*
        if (Input.GetKey(KeyCode.S))
        {
            Physics2D.IgnoreLayerCollision(player, plat, true);
        }
        */
            //Regula el salto
            if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, FuerzaSalto);
            enSuelo = false;
        }

        //esto regula la transformación. Si se spamea en carrera, la forma humana se queda con el sprite de correr y no sale de ese. Hay que pulirlo.
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (demon)
                demon = false;
            else
                demon = true;
          
        }
      
    }
}
