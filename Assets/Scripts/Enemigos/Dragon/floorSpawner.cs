using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class floorSpawner : MonoBehaviour {
    public GameObject suelo;
    private float counter;
    public float timeToSpawn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if (counter > timeToSpawn)
        {
            GameObject localboi=Instantiate(suelo, GetComponent<Transform>().position, GetComponent<Transform>().rotation);
            try
            {
                localboi.GetComponent<ComportamientoBloqueSuelo>().lifeTime = 25;
            }
            catch (NullReferenceException)
            {
                localboi.GetComponentInChildren<PlataformaAtravesable>().lifeTime = 25;
            }
            counter = 0;
        }
	}
}
