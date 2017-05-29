using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscualoDmg : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("puñodem"))
        {
            GetComponentInParent<ComportamientoEnemigo>().harm(GetComponent<Transform>().position.x>
                collision.GetComponent<Transform>().position.x);
        }
    }
}
