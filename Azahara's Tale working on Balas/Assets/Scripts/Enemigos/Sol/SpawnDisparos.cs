using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDisparos : MonoBehaviour {
    public GameObject disparo;
    private float counter=0;
    private bool curve;
    public float rotSpeed;
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if (counter >= 2.5f)
        {
            GameObject disparo1 = Instantiate(disparo, GetComponent<Transform>().position, new Quaternion(0f, 0f, 0f, 0f));
            //GameObject disparo2 = Instantiate(disparo, GetComponent<Transform>().position, new Quaternion(0f, 0f, 0f, 0f));
            //GameObject disparo3 = Instantiate(disparo, GetComponent<Transform>().position, new Quaternion(0f, 0f, 0f, 0f));
            disparo1.GetComponent<DisparosSol>().Tipo = DisparosSol.estilo.recto;
            /*disparo2.GetComponent<DisparosSol>().Tipo = DisparosSol.estilo.curvo;
            disparo2.GetComponent<DisparosSol>().setRotSpeed(0);
            disparo2.GetComponent<Transform>().Rotate(0f, 0f, 30f);
            disparo3.GetComponent<DisparosSol>().Tipo = DisparosSol.estilo.curvo;
            disparo3.GetComponent<DisparosSol>().setRotSpeed(-0);
            disparo3.GetComponent<Transform>().Rotate(0f, 0f, -30f);*/
            counter = 0;
        }
	}
    
}
