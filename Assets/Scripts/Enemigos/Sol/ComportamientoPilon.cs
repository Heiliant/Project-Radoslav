using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoPilon : MonoBehaviour {
    public int HP;
    private float counter;
    private int check;
    private bool localdmg;
    public GameObject luz;
	// Use this for initialization
	void Start () {
        check = 1;
        localdmg = false;
	}

    private void FixedUpdate()//cuando le arreas ostias al pilón, se pone oscuro. Después, espera 10*2 segundos y vuelve a iluminarse.
    {
        if (HP <= 0)
        {
            if (!localdmg)
            {
                FindObjectOfType<ComportamientoSol>().dmgSun();
                localdmg = true;
            }

            counter += Time.deltaTime*check;
            if (counter<0f)
            {
                counter = 0f;
                check = 1;
                HP = 1;
                localdmg = false;
            }
            if(counter<=1.25f && check==1)
                GetComponent<SpriteRenderer>().color = new Color(1-counter/1.6f, 1-counter/1.6f, 1-counter/1.6f);
            else if(counter<=1.25 && check==-1)
                GetComponent<SpriteRenderer>().color = new Color(1 - counter / 1.6f, 1 - counter / 1.6f, 1 - counter / 1.6f);
            if (counter >= 10f+Random.Range(-3f, 3f))
            {
                check = -1;
            }
        }

        luz.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1-counter);

    }
    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("puñodem"))
            HP--;
    }
}
