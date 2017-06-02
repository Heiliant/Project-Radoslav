using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class madefg : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.L))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, speed));
        }
	}
}
