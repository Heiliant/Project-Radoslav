using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textDisplay : MonoBehaviour {
    public string text1;
    public string text2;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag.Equals("humana") || collision.tag.Equals("demonio"))
        {
            
            GetComponentInChildren<TextMesh>().text = text1+ "\n" + text2 ;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponentInChildren<TextMesh>().text = "";
    }
}
