using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deflect : MonoBehaviour {
    public GameObject deflectThis;
    private GameObject PJ;

    private void Start()
    {
        PJ = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(deflectThis.tag))
        {
            collision.GetComponent<Rigidbody2D>().velocity = (collision.GetComponent<Transform>().position - PJ.GetComponent<Transform>().position)*30;
        }
    }
}
