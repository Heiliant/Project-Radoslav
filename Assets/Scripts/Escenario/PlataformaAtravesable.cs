using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlataformaAtravesable : MonoBehaviour {
    public GameObject player;
   
    public bool ignorar; //esta variable es de debug, se puede borrar pero ayuda a comprobar las posiciones

    public PlayerControl script;
    public CambioFormas scriptFormas;
    public GameObject HumanBody;
    public float lifeTime = 0;
    private bool doIt;
	void Start () {
        if (lifeTime == 0)
            doIt = false;
        else
            doIt = true;
        player = GameObject.FindGameObjectWithTag("Player");
        HumanBody = GameObject.FindGameObjectWithTag("humana");
        script = player.GetComponent<PlayerControl>();
        scriptFormas = player.GetComponent<CambioFormas>();
        //height = this.transform.Find("ladrillos").gameObject;
    }

    private void FixedUpdate()
    {
        if (script!=null && scriptFormas!=null) {
            if (!scriptFormas.demon)
            {
                if (scriptFormas.DetectorSuelo.position.y > GetComponent<Transform>().position.y) //PJ está por encima de la plataforma
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
                else if (script.DetectorTecho.position.y < GetComponent<Transform>().position.y) //PJ está debajo de la plataforma
                {
                    try
                    {
                        Physics2D.IgnoreCollision(GetComponentInChildren<BoxCollider2D>(), HumanBody.GetComponent<CapsuleCollider2D>(), true);
                    }
                    catch (NullReferenceException) { };
                    ignorar = true;
                }
            }
            else
            {
                if (scriptFormas.DetectorSuelo.position.y > GetComponent<Transform>().position.y) //PJ está por encima de la plataforma
                {
                    if (Input.GetKey(KeyCode.S))
                    {
                        try
                        {
                            Physics2D.IgnoreCollision(GetComponentInChildren<BoxCollider2D>(), GameObject.FindGameObjectWithTag("demonio").GetComponent<CapsuleCollider2D>(), true);
                        }
                        catch (NullReferenceException) { };
                        ignorar = true;

                    }
                    else
                    {
                        try
                        {
                            Physics2D.IgnoreCollision(GetComponentInChildren<BoxCollider2D>(), GameObject.FindGameObjectWithTag("demonio").GetComponent<CapsuleCollider2D>(), false);
                        }
                        catch (NullReferenceException) { };
                        ignorar = false;

                    }
                }
                else if (script.DetectorTecho.position.y < GetComponent<Transform>().position.y) //PJ está debajo de la plataforma
                {
                    Physics2D.IgnoreCollision(GetComponentInChildren<BoxCollider2D>(), GameObject.FindGameObjectWithTag("demonio").GetComponent<CapsuleCollider2D>(), true);
                    ignorar = true;
                }
            }
        }
    }
    private void Update()
    {
        if (doIt)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }

    }
}

