using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoDragon : MonoBehaviour {
    public GameObject[] brazos;
    public GameObject[] antebrazos;
    public GameObject player;
    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        foreach (GameObject a in brazos)
        {
            //vector unitario del hombro al PJ
            Vector2 localVector = a.GetComponent<Transform>().position - player.GetComponent<Transform>().position;
            localVector /= (Vector2.Distance(a.GetComponent<Transform>().position, player.GetComponent<Transform>().position));

            //angulo entre localVec y Vector2.right
            float angle=Mathf.Acos(((Vector2.right.x * localVector.x) + (Vector2.right.y * localVector.y)) / 
                (Mathf.Sqrt(Vector2.right.x * Vector2.right.x + Vector2.right.y * Vector2.right.y) * 
                Mathf.Sqrt(localVector.x * localVector.x + localVector.y * localVector.y)));
            angle *= Mathf.Rad2Deg;
            Physics2D.Linecast(a.GetComponent<Transform>().position, player.GetComponent<Transform>().position);
            Debug.DrawLine(a.GetComponent<Transform>().position, player.GetComponent<Transform>().position, Color.red);
            a.GetComponent<Transform>().Rotate(0, 0, angle);

        }
        foreach (GameObject a in antebrazos)
        {
            Physics2D.Linecast(a.GetComponent<Transform>().position, player.GetComponent<Transform>().position);
            Debug.DrawLine(a.GetComponent<Transform>().position, player.GetComponent<Transform>().position, Color.cyan);
        }
    }
}
