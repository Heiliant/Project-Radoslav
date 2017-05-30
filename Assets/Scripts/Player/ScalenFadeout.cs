using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalenFadeout : MonoBehaviour {
    public float GrowthSpeed;
    public float FadeOutSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().localScale = new Vector2(GetComponent<Transform>().localScale.x+Time.deltaTime*GrowthSpeed, 
            GetComponent<Transform>().localScale.y + Time.deltaTime*GrowthSpeed);
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g,
            GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a-Time.deltaTime*FadeOutSpeed);
        if (GetComponent<SpriteRenderer>().color.a <= 0)
            Destroy(gameObject);
	}


}
