using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            localboi.GetComponent<ComportamientoBloqueSuelo>().lifeTime = 25;
            counter = 0;
        }
	}
}
