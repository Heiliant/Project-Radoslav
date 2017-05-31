using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoDisparosDragon : MonoBehaviour {
    private Rigidbody2D rbody;
    private float xForce;
    private float yForce;
    public float strength;
    public Vector2 v0;
    public float timeToShoot;
    public float waitTime;
    public float lifeTime;
    public GameObject PJ;
    float counter = 0;
    bool local = false;
	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        PJ = GameObject.FindGameObjectWithTag("Player");

        rbody.velocity=(v0);
    }
	
	// Update is called once per frame
	void Update () {
        
        counter += Time.deltaTime;
        Vector2 dist = PJ.GetComponent<Transform>().position - GetComponent<Transform>().position;

        if (counter > timeToShoot && !local)
        {
            local = true;
            rbody.velocity = Vector2.zero;
        }
        else if (counter> timeToShoot+waitTime)
        {
            rbody.AddForce(dist*strength);
        }         
        if (counter > lifeTime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("humana") || collision.tag.Equals("demonio"))
        {
            FindObjectOfType<CambioFormas>().attackPlayer(-1);
        }
    }
}


