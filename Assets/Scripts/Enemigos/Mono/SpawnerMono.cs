using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMono : MonoBehaviour {
    public GameObject mono;
    public ComportamientoCamara cam;
    // Use this for initialization
    void Start()
    {
        GetComponent<Transform>().position = new Vector3(139.9f, 46.4f, 20f);
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.tag.Equals("humana"))
        {
            FindObjectOfType<LevelManagment>().boss=Instantiate(mono, GetComponent<Transform>().position, new Quaternion(0, 0, 0, 1));
            cam.bossSize();
            GameObject.FindObjectOfType<ComportamientoMono>().startFight();
            GetComponent<BoxCollider2D>().enabled=(false);
        }
    }
    
}
