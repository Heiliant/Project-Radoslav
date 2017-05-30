using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenuOpciones : MonoBehaviour {
    public GameObject changeControls;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void _Controles(){
        changeControls.SetActive(true);
	}

    public void _xd()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=L2tGu3tKIFM");
    }

    public void _Atras()
    {
        gameObject.SetActive(false);
    }
}
