using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarATQEscualo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("humana") || collision.tag.Equals("demonio"))
        {
            float localRecoil;
            if (GetComponent<Transform>().position.x > collision.GetComponent<Transform>().position.x)
                localRecoil = -1;
            else
                localRecoil = 1;
            FindObjectOfType<CambioFormas>().attackPlayer(localRecoil);
        }
    }
}
