using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDragon : MonoBehaviour {
    public GameObject[] puente;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("humana") || collision.tag.Equals("demonio")) {
            FindObjectOfType<ComportamientoDragonMovimiento>().modo = ComportamientoDragonMovimiento.estado.comienzo;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>().startMoveB = true;
            foreach (GameObject a in puente)
            {
                a.SetActive(false);
            }
            Destroy(this);
        }
    }
}
