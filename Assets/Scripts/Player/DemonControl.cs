﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonControl : MonoBehaviour {
    public float FuerzaPasos;
    public float FuerzaSalto;

    private KeyCode JUMP = KeyCode.Space;
    private KeyCode LEFT = KeyCode.A;
    private KeyCode RIGHT = KeyCode.D;
    private KeyCode DOWN = KeyCode.S;
    private KeyCode PUÑO = KeyCode.Mouse0;

    public Animator animacionDemon;

    private bool enSuelo;
    private bool inmovil;
    private bool puñetazo;

    private float segundero;

    public void stayQuiet(bool a)
    {
        inmovil = a;
    }

    // Use this for initialization
    void Start () {
        //animacionDemon = GameObject.FindGameObjectWithTag("demonio").GetComponent<Animator>();
	}

    private void FixedUpdate()
    {
        enSuelo = FindObjectOfType<CambioFormas>().enSuelo;

        animacionDemon.SetFloat("VelX", GetComponent<Rigidbody2D>().velocity.x);
        animacionDemon.SetBool("enSuelo", enSuelo);
        animacionDemon.SetBool("puñetazo", puñetazo);

        if (GetComponent<Rigidbody2D>().velocity.x >0.1f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 1f);
        else if (GetComponent<Rigidbody2D>().velocity.x <(-0.1f))
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 1f);

        if (puñetazo)
        {
            segundero += Time.deltaTime; inmovil = true;
            if (segundero >= 1.4f)
            {
                puñetazo = false;
                segundero = 0;
                inmovil = false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!inmovil)
        {
            if (Input.GetKey(LEFT) && enSuelo)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-FuerzaPasos, GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (Input.GetKey(RIGHT) && enSuelo)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(FuerzaPasos, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                if (GetComponent<Rigidbody2D>().velocity.x > 0.1)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-FuerzaPasos, 0));
                }
                else if (GetComponent<Rigidbody2D>().velocity.x < -0.1)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(FuerzaPasos, 0));
                }
            }

            if (Input.GetKeyDown(JUMP) && enSuelo)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, FuerzaSalto));
            }

            if (GetComponent<Rigidbody2D>().velocity.x > -0.1f && GetComponent<Rigidbody2D>().velocity.x < 0.1f && Input.GetKeyDown(PUÑO))
                puñetazo = true;

            
        }
    }
}
