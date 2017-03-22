using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAtravesable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            if (Input.GetKey(KeyCode.DownArrow))
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);               
        }            
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, false);
    }
}

