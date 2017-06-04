using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour {

    public Transform Prota;
    public Transform Boca_Canon;
    private float distanciaAlPlayer;   //lo que hacemos es un triangulo
    public GameObject Bala;
    public float TiempoRecarga=2.5f;
    private float tiempoActual;
    public float rad;
    private bool BalaInstanciada;
    Vector2 SalidaBala;

    public Vector2[] tramos;

    Vector2 Canon_Player;

    public float hp;
    private float counter = 0;
    public float deathTime;

	void Start () {
        BalaInstanciada = false;

        Prota = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        tiempoActual = TiempoRecarga;
	}


    private void FixedUpdate()
    {
        if (hp <= 0)
        {

            counter += Time.deltaTime;
            foreach(SpriteRenderer a in GetComponentsInChildren<SpriteRenderer>())
            {
                a.color = new Color(1, 1, 1, 1-(counter/deathTime));
            }
            if (counter >= deathTime)
            {
                Destroy(gameObject);
            }
        }
    }

    void Update ()
    {
        if (hp > 0)
        {
            Canon_Player = Prota.position - transform.position;


            distanciaAlPlayer = Vector2.Distance(transform.position, Prota.position);

            if (distanciaAlPlayer <= rad)
            {
                SigueCanion(Canon_Player);

            }
        }
    }

    void SigueCanion(Vector2 a)
    {
        float resultado;
        
        resultado = Mathf.Rad2Deg*(Mathf.Atan2(a.y, a.x))-180;

        while (resultado <= 0)
            resultado += 360;

        while (resultado > 360)
            resultado -= 360;

        for (int i = 0; i < tramos.Length; ++i)
        {
            if (resultado >= tramos[i].x && resultado <= tramos[i].y)
            {
                GetComponent<Transform>().rotation = Quaternion.AngleAxis(resultado, Vector3.forward);
                InstanciarBala();
            }
        }
    }

    void InstanciarBala()
    {
        tiempoActual -= Time.deltaTime;
        if (tiempoActual <= 0.0f)
        {
            BalaInstanciada = false;
            if (!BalaInstanciada)
            {
                BalaInstanciada = true;
                tiempoActual = TiempoRecarga;
                GameObject shot=Instantiate(Bala, Boca_Canon.position, GetComponent<Transform>().rotation);
                shot.GetComponent<Transform>().Rotate(0, 0, -90);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag.Equals("demonshot"))
        {
            collision.gameObject.GetComponent<demonShotBehaviour>().explode();
            Destroy(collision.gameObject);
            hp-=0.5f;
        }
        if (collision.tag.Equals("puñodem"))
        {
            hp -= 10;
        }
    }

}
