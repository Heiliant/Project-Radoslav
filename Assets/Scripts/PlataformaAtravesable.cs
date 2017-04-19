using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAtravesable : MonoBehaviour {
    public GameObject player;
   
    public bool ignorar; //esta variable es de debug, se puede borrar pero ayuda a comprobar las posiciones

    public PlayerControl script;
    public GameObject DemonBody;
    public GameObject HumanBody;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        HumanBody = GameObject.FindGameObjectWithTag("muñeco");
        DemonBody = GameObject.FindGameObjectWithTag("torso");
        script = player.GetComponent<PlayerControl>();
        //height = this.transform.Find("ladrillos").gameObject;
    }

    private void FixedUpdate()
    {
        if (script.Humana)
        {
            if (script.DetectorSuelo.position.y + 0.1f > (GetComponent<Transform>().position.y +
                (GetComponent<BoxCollider2D>().size.y) / 5)) //PJ está por encima de la plataforma
            {
                if (Input.GetKey(KeyCode.S))
                {
                    Physics2D.IgnoreCollision(GetComponentInChildren<BoxCollider2D>(), HumanBody.GetComponent<CapsuleCollider2D>(), true);
                    ignorar = true;
                }
                else
                {
                    Physics2D.IgnoreCollision(GetComponentInChildren<BoxCollider2D>(), HumanBody.GetComponent<CapsuleCollider2D>(), false);
                    ignorar = false;

                }
            }
            else if (script.DetectorTecho.position.y +0.1f< GetComponent<Transform>().position.y -
                (GetComponent<BoxCollider2D>().size.y) / 5) //PJ está debajo de la plataforma
            {
                Physics2D.IgnoreCollision(GetComponentInChildren<BoxCollider2D>(), HumanBody.GetComponent<CapsuleCollider2D>(), true);
                ignorar = true;
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}

    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
                if (Input.GetKey(KeyCode.DownArrow))
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
                else
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, false);
               
        }            
    }

    
    private void OnCollisionExit2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, false);
    }*/
}

