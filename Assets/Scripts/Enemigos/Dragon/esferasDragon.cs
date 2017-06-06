using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esferasDragon : MonoBehaviour {
    public GameObject efectoCarga;
    public GameObject dragonShots;
    public float timeToAttack;
    public float variacion;
    private float counter = 0;
    private float actualTime;
    private int checker = 0;
    public int amountOfShots;
    private GameObject[] shots;
    public float shotForce=15;
	// Use this for initialization
	void Start () {
        actualTime = timeToAttack + (Random.Range(-variacion, variacion));
        shots = new GameObject[amountOfShots];
	}
	
	// Update is called once per frame
	void Update () {
        if (FindObjectOfType<dragonDamage>().dragonHP>0) {
            counter += Time.deltaTime;
            if (counter > actualTime)
            {
                for (int i = 0; i < amountOfShots; ++i)
                {
                    shots[i] = Instantiate(dragonShots, GetComponent<Transform>().position, GetComponent<Transform>().rotation);
                    shots[i].GetComponent<ComportamientoDisparosDragon>().strength = 4;
                    shots[i].GetComponent<ComportamientoDisparosDragon>().v0 = new Vector2(Mathf.Sin(i) * shotForce, Mathf.Cos(i) * shotForce);
                    shots[i].GetComponent<ComportamientoDisparosDragon>().timeToShoot = 1;
                    shots[i].GetComponent<ComportamientoDisparosDragon>().waitTime = 1;
                    shots[i].GetComponent<ComportamientoDisparosDragon>().lifeTime = 7 * 0.67f;
                }
                counter = 0;
                actualTime = timeToAttack + (Random.Range(-variacion, variacion));
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(counter / actualTime, counter / actualTime, counter / actualTime);
            }
        }
	}
}
