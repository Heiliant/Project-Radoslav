using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZonaI : MonoBehaviour {

    public bool IisTriggered;

    void Start()
    {
        IisTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "humana")
            IisTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "humana")
            IisTriggered = false;
    }
}