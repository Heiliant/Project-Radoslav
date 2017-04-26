using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZonaC : MonoBehaviour {
    public bool CisTriggered;

    void Start()
    {
        CisTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "humana")
            CisTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "humana")
            CisTriggered = false;
    }
}
