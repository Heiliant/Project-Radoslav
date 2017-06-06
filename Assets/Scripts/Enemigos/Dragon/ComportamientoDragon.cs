using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoDragon : MonoBehaviour {
    public GameObject[] brazos;
    public GameObject[] antebrazos;
    public GameObject[] manos;
    public GameObject player;
    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < antebrazos.Length; ++i)
        {
            //vector unitario del hombro al PJ
            Vector2 localVector = antebrazos[i].GetComponent<Transform>().position - player.GetComponent<Transform>().position;
            localVector /= (Vector2.Distance(antebrazos[i].GetComponent<Transform>().position, player.GetComponent<Transform>().position));


            float angle = Mathf.Atan2(localVector.y, localVector.x);
            angle *= Mathf.Rad2Deg;
            angle -= 90;

           

                antebrazos[i].GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle, Vector3.forward);
   
        }

        for (int i = 0; i < brazos.Length; ++i)
        {
            //vector unitario del hombro al PJ
            Vector2 localVector = brazos[i].GetComponent<Transform>().position - player.GetComponent<Transform>().position;
            localVector /= (Vector2.Distance(brazos[i].GetComponent<Transform>().position, player.GetComponent<Transform>().position));


            float angle = Mathf.Atan2(localVector.y, localVector.x);
            angle *= Mathf.Rad2Deg;
            angle -= 90;
            
            while (angle < 0)
                angle += 360;
            while (angle >= 360)
                angle -= 360;

            if (brazos[i].name.Equals("BrazoD4"))
            if (i % 2 == 0)
            {
                if ((angle >= 60 && angle <= 120))
                    brazos[i].GetComponent<Transform>().rotation = Quaternion.AngleAxis(Mathf.Clamp(angle, 60, 120), Vector3.forward);
                else if ((angle <= 300 && angle >= 240))
                    brazos[i].GetComponent<Transform>().rotation = Quaternion.AngleAxis(Mathf.Clamp(360 - angle, 60, 120), Vector3.forward);
            }
            
            else
            {
                if ((angle >= 60 && angle <= 120))
                    brazos[i].GetComponent<Transform>().rotation = Quaternion.AngleAxis(Mathf.Clamp(360-angle, 240, 300), Vector3.forward);
                else if ((angle <= 300 && angle >= 240))
                    brazos[i].GetComponent<Transform>().rotation = Quaternion.AngleAxis(Mathf.Clamp(angle, 240, 300), Vector3.forward);
            }

        }
    }
    
}
