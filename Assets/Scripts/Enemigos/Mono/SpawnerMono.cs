using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMono : MonoBehaviour {
    public GameObject mono;
    public ComportamientoCamara cam;
    // Use this for initialization
    void Start()
    {
        GetComponent<Transform>().position = new Vector3(139.9f, 46.4f, 0f);
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.tag == "humana")
        {
            Instantiate(mono, GetComponent<Transform>());
            cam.bossSize();
            GameObject.FindObjectOfType<ComportamientoMono>().startFight();
            Destroy(this);
        }
    }
    
}
