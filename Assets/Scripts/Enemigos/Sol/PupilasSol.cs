using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupilasSol : MonoBehaviour {
    private Transform PJ;

	// Use this for initialization
	void Start () {
		
	}
	
	
	void Update () {
        PJ = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Debug.DrawLine(GetComponent<Transform>().position, PJ.position, Color.cyan);
        RaycastHit2D colision=Physics2D.Linecast(GetComponent<Transform>().position, PJ.position);

        if (colision.collider != null)
        {
            foreach(Transform a in GetComponentInChildren<Transform>())
            {
                if (a.name != gameObject.name)
                    a.position = colision.point;
            }
        }
	}
}
