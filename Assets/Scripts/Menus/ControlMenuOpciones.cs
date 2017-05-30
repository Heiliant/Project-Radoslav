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

    public void _Sonido()
    {

    }

    public void _Atras()
    {
        gameObject.SetActive(false);
    }
}
