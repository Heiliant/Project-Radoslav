using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDisparos : MonoBehaviour {
    public GameObject disparo;
    private float counter=4;
    private bool curve;
    public float rotSpeed;
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if (counter >= 4f)
        {
            GameObject disparo1 = Instantiate(disparo, GetComponent<Transform>().position, new Quaternion(0f, 0f, 0f, 0f));
            disparo1.GetComponent<DisparosSol>().Tipo = DisparosSol.estilo.recto;
            counter = 0;
        }
	}
    
}
