using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoderSpawn : MonoBehaviour {
    public Transform destiny;
    public GameObject relevant;
    public float velocity;
    private string textaco;
    public enum boss{
        mono,
        sol
    }
    public boss tipo;
    private bool localB = false;
    private bool localSumadre = false;
    private float counter = 0;
	void Start () {
		foreach(Light a in GetComponentsInChildren<Light>())
        {
            a.intensity = 0;
        }
	}
	void Update () {
        foreach (Light a in GetComponentsInChildren<Light>())
        {
            if (a.intensity < 8)
                a.intensity += 0.06f;
        }

        float sentido;
        if (GetComponent<Transform>().position.y > destiny.position.y)
            sentido = -1;
        else
            sentido = 1;
        GetComponent<Transform>().Translate(0f, sentido*(velocity*Mathf.Abs((destiny.position.y-GetComponent<Transform>().position.y)))/1000, 0f);
        destiny.transform.Translate(0, sentido * -(velocity * Mathf.Abs((destiny.position.y - GetComponent<Transform>().position.y))) / 1000, 0);

        if (localB)
        {
            counter += Time.deltaTime; 
            relevant.GetComponent<Image>().color = new Color(1, 1, 1, counter);
         
        }

        if (counter >= 1)
        {
            relevant.GetComponentInChildren<Text>().text = textaco;
            Time.timeScale = 0;
            localB = false;
            if (!localSumadre)
            {
                if (!Input.anyKey)
                    localSumadre = true;
            }
        }

        if (Input.anyKey && Time.timeScale==0 && !localB && localSumadre)
        {
            relevant.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            relevant.GetComponentInChildren<Text>().text = "";
            relevant.SetActive(false);
            Time.timeScale = 1;
            localB = false;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (tipo) {
            case boss.mono:
                FindObjectOfType<CambioFormas>().enableTransf();
                FindObjectOfType<PlayerControl>().setAmountOfJumps(1);
                textaco = "Has obtenido nuevas habilidades.\n\n\n" +
                    "-Pulsa E para transformarte en demonio.\n\n-Pulsa dos veces ESPACIO para ejecutar un doble salto" +
                    "\n\n\t\t\t\t\t\t\tpulsa cualquier tecla para continuar";
            break;
            case boss.sol:
                FindObjectOfType<CambioFormas>().disparoSkill=true;
                textaco = "Has obtenido nuevas habilidades.\n\n" +
                    "-Pulsa MouseDerecho para usar magia. La magia varía en función de tu forma:\n\t·En humana, es un pulso que repele proyectiles" +
                    "\n\t·En demonio son disparos.\n\n\t\t\t\t\t\t\tpulsa cualquier tecla para continuar";
                break;
        }
        
        relevant.SetActive(true);   
        foreach(Light a in GetComponentsInChildren<Light>())
        {
            Destroy(a);
        }
        Destroy(GetComponentInChildren<ParticleSystem>());
        localB = true;
        
    }   
}
