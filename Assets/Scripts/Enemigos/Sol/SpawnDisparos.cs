using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDisparos : MonoBehaviour {
    public GameObject disparo;
    private float counter=0;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if (counter >= 1.5f)
        {
            Instantiate(disparo, GetComponent<Transform>().position, new Quaternion(0f, 0f, 0f, 0f));
            counter = 0;
        }
	}
    
}
