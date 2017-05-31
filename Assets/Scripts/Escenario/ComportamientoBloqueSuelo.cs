using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoBloqueSuelo : MonoBehaviour {
    public  bool movVer=true;
    public float fromMinVal;
    public float toMaxVal;
    public float speed = 0.001f;
    private float Xini;
    private float Yini;
    private float counter = 0;
    public float lifeTime;
    // Use this for initialization

    void Start () {
        Xini = GetComponent<Transform>().position.x;
        Yini = GetComponent<Transform>().position.y;
    }

    // Update is called once per frame
    void Update () {
        if (movVer)
        {
            if (GetComponent<Transform>().position.y >= (Yini+toMaxVal) || GetComponent<Transform>().position.y <= (Yini-fromMinVal))
            {
                speed *= -1;
            }
                transform.Translate(new Vector2(0, speed));
        }
        else
        {
            if (GetComponent<Transform>().position.x >= (toMaxVal+Xini) || GetComponent<Transform>().position.x <= (Xini-fromMinVal))
            {
                speed *= -1;
            }
            transform.Translate(new Vector2(speed, 0));
        }
        if (lifeTime != 0)
        {
            counter += Time.deltaTime;
            if (counter > lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
