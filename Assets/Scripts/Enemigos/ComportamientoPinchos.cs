using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoPinchos : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("humana") || collision.tag.Equals("demonio"))
        {
            FindObjectOfType<CambioFormas>().killPlayer();
        }
    }
}
