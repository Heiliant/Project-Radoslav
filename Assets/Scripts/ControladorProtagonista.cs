using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorProtagonista : MonoBehaviour {
    private float VelX;
    private Animator animator; //creo un objeto "animator" donde meteré el componente animador del personaje
    public float FuerzaPasos = 10f;  //variables publicas para el movimiento y el salto. Por si lo queremos tocar dentro de unity directamente.
    public float FuerzaSalto = 20f;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>(); //meto el componente animador dentro del objeto "animator"
	}
	
    void FixedUpdate()
    {
        animator.SetFloat("VelX", VelX); //Cada X fotogramas, accedo al "animator", el cual contiene el animador del personaje
                                         // y accedo a la variable VelX y le doy el valor de la variable del codigo VelX (la segunda), que
                                         // se actualiza en el update.
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
        VelX = GetComponent<Rigidbody2D>().velocity.x; //VelX vale la velocidad del personaje en el eje X. Este dato se actualiza cada fotograma.

        //Hace falta meter algún control que impida saltar si no está en el suelo. Esto es provisional.
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, FuerzaSalto);
        }
    }
}
