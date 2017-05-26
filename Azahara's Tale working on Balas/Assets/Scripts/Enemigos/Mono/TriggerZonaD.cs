using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZonaD : MonoBehaviour {

    private bool DisTriggered;

    void Start()
    {
        DisTriggered = false;
    }

    public bool getD()
    {
        return DisTriggered;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "humana")
            DisTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "humana")
            DisTriggered = false;
    }
}