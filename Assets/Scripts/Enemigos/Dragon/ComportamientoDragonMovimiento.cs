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
                if(counter<3.5f)
                    counter += Time.deltaTime;
                GetComponent<Transform>().Translate(1f*vel, 0f, 0f);
                if (counter > 3)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().stayQuiet(false);
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().Translate(1f * vel, 0f, 0f);
                    
                }
                break;
        }
	}
}
