using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSunFight : MonoBehaviour {
    public AudioClip musicaSol;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("humana") || collision.tag.Equals("demonio"))
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip=musicaSol;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
            FindObjectOfType<ComportamientoSol>().modo = ComportamientoSol.Estado.nullemod;
            FindObjectOfType<ComportamientoCamara>().startMoveB = true;
            if (!FindObjectOfType<ComportamientoCamara>().startMoveB)
                FindObjectOfType<ComportamientoCamara>().stickTo(GameObject.Find("Solesico").GetComponent<Transform>().position);
            Destroy(gameObject);
        }
    }
}
