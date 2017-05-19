using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparosSol : MonoBehaviour {
    private Vector2 origin;
    public Transform sun;
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
        Vector2 localVec;
        Mathf.an
        localVec = new Vector2(Mathf.Abs(sun.position.x - origin.x), Mathf.Abs(sun.position.y - origin.y));
        float angle= (Vector2.down.x*localVec.x+Vector2.down.y*localVec.y)/(
            (Mathf.Sqrt(Vector2.down.x* Vector2.down.x + Vector2.down.y* Vector2.down.y))*(Mathf.Sqrt(localVec.x*localVec.x+localVec.y*localVec.y)));
        GetComponent<Transform>().Rotate(new Vector3(0, 0, Mathf.Cos(angle)));
        Debug.Log(Mathf.Cos(angle));
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "humana")
        {
            if(GetComponent<Transform>().rotation.x > 0)
                FindObjectOfType<PlayerControl>().attackPlayer(-1);
            else
                FindObjectOfType<PlayerControl>().attackPlayer(1);
        }
    }
}
