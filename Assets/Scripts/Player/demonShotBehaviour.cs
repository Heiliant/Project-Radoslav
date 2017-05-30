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
    void Start()
    {
        from = GetComponent<Transform>().position;
        to = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec = new Vector2(to.x - from.x, to.y - from.y);
        float modulo = Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
        vec = new Vector2((vec.x / modulo) * speed, (vec.y / modulo) * speed);

        Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        GetComponent<Transform>().Translate(vec.x, vec.y, 0);
        if (counter >= 10)
            Destroy(gameObject);
    }
}