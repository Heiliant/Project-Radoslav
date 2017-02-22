using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorProtagonista : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Controles básicos para ir probando cosillas. Falta pulirlos.
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(10f, GetComponent<Rigidbody2D>().velocity.y);
        }

        //Hace falta meter algún control que impida saltar si no está en el suelo. Esto es provisional.
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 20f);
        }
    }
}
