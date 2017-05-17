using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRingmov : MonoBehaviour {
    float counter = 0;
    public float destroyTime;
    public float incVel = 0;
    public float rotSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        GetComponent<Transform>().localScale = new Vector2(GetComponent<Transform>().localScale.x + (counter)*incVel, 
            GetComponent<Transform>().localScale.y + (counter)*incVel);
        GetComponent<Transform>().Rotate(0f, 0f, rotSpeed);
        if (counter >= destroyTime)
            Destroy(gameObject);
	}
}
