using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallController : MonoBehaviour {

    public float speed;

    public GameObject player;

    public Rigidbody2D myrigidbody;

    // Use this for initialization
    void Start() {

        player = FindObjectOfType<GameObject>();

        myrigidbody = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update() {

        myrigidbody.velocity = new Vector2(speed, myrigidbody.velocity.y);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "humana") || (collision.tag == "demonio"))
        {

            player.GetComponent<CambioFormas>().attackPlayer();
        }

    }
}
