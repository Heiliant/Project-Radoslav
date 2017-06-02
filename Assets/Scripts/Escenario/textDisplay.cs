using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textDisplay : MonoBehaviour {
    public string destination;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag.Equals("humana") || collision.tag.Equals("demonio"))
        {
            
            GetComponentInChildren<TextMesh>().text = destination+"\ntodo recto." ;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponentInChildren<TextMesh>().text = "";
    }
}
