using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class deflect : MonoBehaviour {
    public GameObject deflectThis;
    public GameObject deflectThistag_noRB;
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
        else if (collision.tag.Equals(deflectThistag_noRB.tag))
        {
            Debug.Log(collision.tag);
            Vector2 local = collision.GetComponent<Transform>().position - PJ.GetComponent<Transform>().position;
            local/=Vector2.Distance(collision.GetComponent<Transform>().position, PJ.GetComponent<Transform>().position);
            float angle = Mathf.Atan2(local.y, local.x);
            angle *= Mathf.Rad2Deg;
            angle += 90;
            collision.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            try
            {
                collision.GetComponent<disparosDragonImpostor>().speed = 30;
            }
            catch (NullReferenceException)
            {
                try
                {
                    collision.GetComponent<DisparosSol>().speed = 30;
                }
                catch (NullReferenceException)
                {
                    collision.GetComponent<DisparoSolImpostor>().speed = 30;
                }
            }
        }
    }
}
