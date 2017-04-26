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
        HumanBody = GameObject.FindGameObjectWithTag("humana");
        DemonBody = GameObject.FindGameObjectWithTag("torso");
        script = player.GetComponent<PlayerControl>();
        //height = this.transform.Find("ladrillos").gameObject;
    }

    private void FixedUpdate()
    {
        if (script.Humana)
        {
            if (script.DetectorSuelo.position.y > GetComponent<Transform>().position.y) //PJ está por encima de la plataforma
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
            else if (script.DetectorTecho.position.y  < GetComponent<Transform>().position.y) //PJ está debajo de la plataforma
            {
                Physics2D.IgnoreCollision(GetComponentInChildren<BoxCollider2D>(), HumanBody.GetComponent<CapsuleCollider2D>(), true);
                ignorar = true;
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    {

        Debug.Log("DetSuelo: " + script.DetectorTecho.position.y);
        Debug.Log("Caja " + GetComponent<Transform>().position.y);
        Debug.Log("bool: " + (script.DetectorTecho.position.y > GetComponent<Transform>().position.y));
    }
        
}

