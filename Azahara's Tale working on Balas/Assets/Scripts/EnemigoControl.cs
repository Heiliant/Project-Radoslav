using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoControl : MonoBehaviour {
    private Animator escualoAnim;
    private float velX;
    private bool enSuelo;
    private bool atacar;

	// Use this for initialization
	void Start () {
        escualoAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
