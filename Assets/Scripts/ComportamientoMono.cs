using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoMono : MonoBehaviour {
    private GameObject player;

    private EdgeCollider2D palaD;
    private EdgeCollider2D palaI;
     
    private GameObject zonaD; 
    private GameObject zonaI; 
    private GameObject zonaC;

    private Animator animacionMono;

    public int monoHP=3;
    public int tripaHP=10;

    public float localcolor=1;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        palaD = GameObject.Find("Pala D").GetComponent<EdgeCollider2D>();
        palaI = GameObject.Find("Pala I").GetComponent<EdgeCollider2D>();

        zonaD = GameObject.Find("ZonaD");
        zonaI = GameObject.Find("ZonaI");
        zonaC = GameObject.Find("ZonaC");

        GameObject.Find("ParedPalaD").SetActive(false);
        GameObject.Find("ParedPalaI").SetActive(false);

        animacionMono = GetComponent<Animator>();
	}

    private void FixedUpdate()
    {
        
        GameObject.Find("Torso M").GetComponent<SpriteRenderer>().color = new Color(1f, localcolor-=(Time.deltaTime/10), localcolor, 1f);
    }

    private void Update()
    {
        if (zonaD.GetComponent<TriggerZonaD>().DisTriggered)
        {
            animacionMono.SetBool("derecha", true);
            animacionMono.SetBool("izquierda", false);
            animacionMono.SetBool("centro", false);
        }

        else if (zonaI.GetComponent<TriggerZonaI>().IisTriggered)
        {
            animacionMono.SetBool("derecha", false);
            animacionMono.SetBool("izquierda", true);
            animacionMono.SetBool("centro", false);
        }

        else if (zonaC.GetComponent<TriggerZonaC>().CisTriggered)
        {
            animacionMono.SetBool("derecha", false);
            animacionMono.SetBool("izquierda", false);
            animacionMono.SetBool("centro", true);
        }

    }
    
}
