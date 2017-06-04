using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparosDragonImpostor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().Translate(0f, Time.deltaTime * -5, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("demonio") || (collision.tag.Equals("humana")))
        {
            if (GetComponent<Transform>().rotation.x > 0)
                FindObjectOfType<CambioFormas>().attackPlayer(-1);
            else
                FindObjectOfType<CambioFormas>().attackPlayer(1);
        }
    }
}
