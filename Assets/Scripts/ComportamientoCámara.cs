using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoCámara : MonoBehaviour {
    public GameObject Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().position = new Vector3(Player.GetComponent<Transform>().position.x, 
                                                         Player.GetComponent<Transform>().position.y, 
                                                         GetComponent<Transform>().position.z);
	}
}
