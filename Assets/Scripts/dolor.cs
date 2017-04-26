using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dolor : MonoBehaviour {

    private PlayerControl script;

	// Use this for initialization
	void Start () {
        script = FindObjectOfType<PlayerControl>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "humana")
        {
            if((collision.transform.position.x - GetComponent<Transform>().position.x) < 0)
            {
                script.attackPlayer(-1);
            }
            else if((collision.transform.position.x - GetComponent<Transform>().position.x) > 0){
                script.attackPlayer(1);
            }
            
        }
     
    }
}
