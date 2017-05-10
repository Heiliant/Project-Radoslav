using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoderSpawn : MonoBehaviour {
    public Transform destiny;
    public float velocity;

	// Use this for initialization
	void Start () {
		foreach(Light a in GetComponentsInChildren<Light>())
        {
            a.intensity = 0;
        }
	}

    // Update is called once per frame
    

	// Update is called once per frame
	void Update () {
        foreach (Light a in GetComponentsInChildren<Light>())
        {
            if (a.intensity < 8)
                a.intensity += 0.06f;
        }

        float sentido;
        if (GetComponent<Transform>().position.y > destiny.position.y)
            sentido = -1;
        else
            sentido = 1;
        GetComponent<Transform>().Translate(0f, sentido*(velocity*Mathf.Abs((destiny.position.y-GetComponent<Transform>().position.y)))/1000, 0f);
        destiny.transform.Translate(0, sentido * -(velocity * Mathf.Abs((destiny.position.y - GetComponent<Transform>().position.y))) / 1000, 0);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<CambioFormas>().enableTransf();
        Destroy(gameObject);
    }
}
