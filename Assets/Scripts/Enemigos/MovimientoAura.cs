using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAura : MonoBehaviour {
    public float vel;
    public float scaleSpeed;
    public Vector2 maxSize;
    public Vector2 minSize;
    public bool decreaseScale;
	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        if (GetComponent<Transform>().localScale.x < minSize.x && GetComponent<Transform>().localScale.y < minSize.y)
        {
            decreaseScale = false;
        }
        else if (GetComponent<Transform>().localScale.x >maxSize.x && GetComponent<Transform>().localScale.y > maxSize.y)
        {
            decreaseScale = true;
        }

    }

    // Update is called once per frame
    void Update () {
        GetComponent<Transform>().Rotate(0, 0, Time.deltaTime*vel);
        if(!decreaseScale)
            GetComponent<Transform>().localScale = new Vector3(GetComponent<Transform>().localScale.x+Time.deltaTime*scaleSpeed, 
                GetComponent<Transform>().localScale.y+Time.deltaTime*scaleSpeed, 1);
        else
            GetComponent<Transform>().localScale = new Vector3(GetComponent<Transform>().localScale.x - Time.deltaTime*scaleSpeed,
                GetComponent<Transform>().localScale.y - Time.deltaTime*scaleSpeed, 1);
    }

}
