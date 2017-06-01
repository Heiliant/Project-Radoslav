using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoderSpawn : MonoBehaviour {
    public Transform destiny;
    public GameObject relevant;
    public float velocity;
    public string textaco;
    public enum boss{
        mono,
        sol
    }
    public boss tipo;
    private bool localB = false;
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
            relevant.GetComponent<Text>().color = new Color(0, 0, 0, 0+counter/3);
        }
        
        if(counter > 5)
        {
            relevant.GetComponent<Text>().color = new Color(1, 1, 1, 0);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (tipo) {
            case boss.mono:
                FindObjectOfType<CambioFormas>().enableTransf();
                FindObjectOfType<PlayerControl>().setAmountOfJumps(1);               
            break;
            case boss.sol:
                FindObjectOfType<CambioFormas>().disparoSkill=true;
                break;
        }
        relevant.GetComponent<Text>().text = textaco;
        Destroy(GetComponent<CircleCollider2D>());
        foreach(Light a in GetComponentsInChildren<Light>())
        {
            Destroy(a);
        }
        Destroy(GetComponentInChildren<ParticleSystem>());
        localB = true;
    }   
}
