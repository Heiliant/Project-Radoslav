using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparosSol : MonoBehaviour {
    private Vector2 origin;
    public float destroyDist=60f;
	// Use this for initialization
	void Start () {

        origin=GetComponent<Transform>().position;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().Translate(0f, Time.deltaTime*-5, 0f);

        if (origin.y - GetComponent<Transform>().position.y > destroyDist)
            Destroy(gameObject);
	}
}
