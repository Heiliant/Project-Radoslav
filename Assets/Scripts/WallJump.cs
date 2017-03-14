using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallJump : MonoBehaviour {
    public GameObject prota;            //Aqui meto el gameobject del PJ para poder retocar sus propiedades desde este script
    public ControladorProtagonista script; //Esto del script sirve para acceder al script "ControladorProtagonista" y asi poder hacer el booleno
                                            // que contiene y que regula el sistema de salto sea true cuando el PJ toca una pared

	// Use this for initialization
	void Start () {
        script = prota.GetComponent<ControladorProtagonista>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D collision) //OnCollisionStay2D se llama cuando el collider de este gameObject entra en contacto con otro
                                                           //collider. El gameobject de este collider se trata a través de "collision"
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) //hay que retocar el script para que al mover al PJ en direccion opuesta a la pared
                                                        //este no se mantenga en el aire por un momento antes de caer.
            {
                prota.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);  //Esto es para que el PJ no se caiga al tocar la pared. 
                                                                                //Aun asi resbala un poquito, pero creo que queda bien. Se puede retocar.
                script.setenSuelo(true);
            }
        }
    }
}
