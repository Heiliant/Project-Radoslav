using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoDragonMovimiento : MonoBehaviour {
    public float vel;
    public enum estado
    {
        comienzo,
        comienzo2,
        mover,
        mover2,
        nulo,
    }
    public GameObject destinyIni;
    public GameObject spawner;
    public estado modo;
    public float timeToMove;
    Vector2 movement;
    float counter = 0;
    // Use this for initialization
    void Start () {
        modo = estado.nulo;
        foreach(esferasDragon a in GetComponentsInChildren<esferasDragon>())
                    {
            a.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {       
        switch (modo)
        {
            case estado.nulo:
                break;
            case estado.comienzo:movement = (destinyIni.GetComponent<Transform>().position - GetComponent<Transform>().position);
                modo = estado.comienzo2;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().stayQuiet(true);
                break;
            case estado.comienzo2:
                if (Vector2.Distance(GetComponent<Transform>().position, destinyIni.GetComponent<Transform>().position) > 2)
                {
                    GetComponent<Transform>().Translate(movement.normalized);
                }
                else
                    modo = estado.mover;
                break;
            case estado.mover: counter += Time.deltaTime;
                if (counter > timeToMove)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>().breakFree(true);
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>().bossSizeCam *= 2;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>().bossYCam *= 2;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>().startMoveB = true;
                    modo = estado.mover2;
                    spawner.GetComponent<floorSpawner>().enabled=(true);
                    counter = 0;
                }
                break;
            case estado.mover2:
                    counter += Time.deltaTime;
                GetComponent<Transform>().Translate(1f*vel, 0f, 0f);
                if (counter >= 5)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().stayQuiet(false);
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().Translate(1f * vel, 0f, 0f);
                    if(counter>13 && counter<14)
                    foreach (esferasDragon a in GetComponentsInChildren<esferasDragon>())
                    {
                        a.enabled = true;
                        a.timeToAttack = Random.Range(5, 15f);
                        a.variacion = 3f;
                        a.amountOfShots = Random.Range(2, 4);
                    }
                    
                }
                else if(counter > 22)
                {
                    foreach (esferasDragon a in GetComponentsInChildren<esferasDragon>())
                    {
                        a.enabled = false;
                    }
                    counter = 5;
                }
                break;
        }
	}
}
