using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparosSol : MonoBehaviour {
    private Vector2 origin;
    public float destroyTime = 10f;
    public enum estilo
    {
        curvo, recto
    };
    public estilo Tipo;
    private float rotSpeed;

    public void setRotSpeed( float a)
    {
        rotSpeed = a;
    }
	// Use this for initialization
	void Start () {

        origin=GetComponent<Transform>().position;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().Translate(0f, Time.deltaTime*-5, 0f);
        if (Tipo == estilo.curvo)
            GetComponent<Transform>().Rotate(0f, 0f, Time.deltaTime*rotSpeed); 


        if (destroyTime<=0f)
            Destroy(gameObject);
        destroyTime -= Time.deltaTime;
	}
}
