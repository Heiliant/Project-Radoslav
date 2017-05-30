using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demonShotBehaviour : MonoBehaviour
{
    private Vector2 from;
    private Vector2 to;
    public float speed;
    private float counter = 0;
    // Use this for initialization
    Vector2 vec;
    public GameObject explosion;

    private float modulo(Vector2 a)
    {
        return Mathf.Sqrt((a.x*a.x)+(a.y*a.y));
    }
    void Start()
    {
        from = GetComponent<Transform>().position;
        to = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec = new Vector2(to.x - from.x, to.y - from.y);
        
        vec = new Vector2((vec.x / modulo(vec)), (vec.y / modulo(vec)));
        
        float angle=Mathf.Acos((Vector2.up.x*vec.x)+(Vector2.up.y*vec.y)/(modulo(Vector2.up)*(modulo(vec))));
        angle *= 360;
        angle /= (2*Mathf.PI);
        if (to.x > from.x)
            angle *= -1;
        transform.Rotate(0f, 0f, angle);
        Debug.Log(angle);
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
            GetComponent<Transform>().Translate(0, speed, 0);
        if (counter >= 5)
            Destroy(gameObject);
    }

    public void explode()
    {
            Instantiate(explosion, GetComponent<Transform>().transform.position, new Quaternion(0, 0, 0, 1));
    }
}