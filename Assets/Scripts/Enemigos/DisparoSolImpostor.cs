using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoSolImpostor : MonoBehaviour {
    private Vector2 origin;
    public Transform sun;
    public float destroyTime = 20f;
    public enum estilo
    {
        curvo, recto
    };
    public estilo Tipo;
    private float rotSpeed;
    public float speed=1;
    public void setRotSpeed(float a)
    {
        rotSpeed = a;
    }
    public void rotateFromSun()
    {
        sun = GameObject.FindGameObjectWithTag("sol").GetComponent<Transform>();
        origin = GetComponent<Transform>().position;
        origin.x -= sun.position.x;
        origin.y -= sun.position.y;
        Vector2 localVec = new Vector2(Vector2.down.x + sun.position.x, Vector2.down.y + sun.position.y);

        float angle = (Vector2.down.x * origin.x + Vector2.down.y * origin.y) / (
            (Mathf.Sqrt(Vector2.down.x * Vector2.down.x + Vector2.down.y * Vector2.down.y)) * (Mathf.Sqrt(origin.x * origin.x + origin.y * origin.y)));
        if (GetComponent<Transform>().position.x < sun.position.x)
            GetComponent<Transform>().Rotate(new Vector3(0, 0, -(Mathf.Acos(angle) * 360 / (2 * Mathf.PI))));
        else
            GetComponent<Transform>().Rotate(new Vector3(0, 0, (Mathf.Acos(angle) * 360 / (2 * Mathf.PI))));
    }

    // Use this for initialization
    void Start()
    {
    }


    void Update()
    {
        GetComponent<Transform>().Translate(0f, Time.deltaTime * -5*speed, 0f);
        if (Tipo == estilo.curvo)
            GetComponent<Transform>().Rotate(0f, 0f, Time.deltaTime * rotSpeed);


        if (destroyTime <= 0f)
            Destroy(gameObject);
        destroyTime -= Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "demonio")
        {
            if (GetComponent<Transform>().rotation.x > 0)
                FindObjectOfType<CambioFormas>().attackPlayer(-1);
            else
                FindObjectOfType<CambioFormas>().attackPlayer(1);
        }
    }
}
