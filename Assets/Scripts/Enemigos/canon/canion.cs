using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canion : MonoBehaviour {

    public Transform Prota;
    public Transform Boca_Canion;
    private float distanciaAlPlayer;   //lo que hacemos es un triangulo
    public Transform Bala;
    private float TiempoRecarga;
    public bool BalaInstanciada;
    Vector2 SalidaBala;

    Vector2 Canon_Player;



	void Start () {
        BalaInstanciada = false;
        TiempoRecarga = 2.5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Canon_Player = transform.position- Prota.position;
       

        distanciaAlPlayer = Vector2.Distance(transform.position, Prota.position);

        if (distanciaAlPlayer<=13.0f)
        {
            SigueCanion(Canon_Player);
            InstanciarBala();
        }

    }

    void SigueCanion(Vector2 a)
    {
        float resultado;

        resultado = Mathf.Rad2Deg*(Mathf.Atan2(a.y, a.x))-180;
        GetComponent<Transform>().rotation = Quaternion.AngleAxis(resultado, Vector3.forward);


    }

    void InstanciarBala()
    {
        TiempoRecarga -= Time.deltaTime;
        if (TiempoRecarga <= 0.0f)
        {
            BalaInstanciada = false;
            if (BalaInstanciada == false)
            {
                BalaInstanciada = true;
                TiempoRecarga = 2.5f;
                Instantiate(Bala, Boca_Canion.position, transform.rotation);
                TiempoRecarga = 2.5f;
            }
        }
    }

}
