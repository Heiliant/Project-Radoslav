using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZonaI : MonoBehaviour {

    private bool IisTriggered;

    void Start()
    {
        IisTriggered = false;
    }

    public bool getI()
    {
        return IisTriggered;
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