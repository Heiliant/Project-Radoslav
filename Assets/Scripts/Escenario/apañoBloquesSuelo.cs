using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apañoBloquesSuelo : MonoBehaviour {

    private void OnCollisionStay2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("demonio")) {
            float speed = GetComponentInParent<ComportamientoBloqueSuelo>().speed;
            if (GetComponentInParent<ComportamientoBloqueSuelo>().movVer)
            {
                collision.gameObject.GetComponent<Transform>().Translate(0, speed, 0);
            }
            else
            {
                collision.gameObject.GetComponent<Transform>().Translate(speed, 0, 0);
            }
        }
    }
}
