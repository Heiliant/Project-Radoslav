using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoCanion : MonoBehaviour
{
    public Transform Prota;
    Vector2 SalidaBala;
	
	// Update is called once per frame
	void Start() {
        SalidaBala = Prota.position - transform.position;
        SalidaBala /= (Mathf.Sqrt(SalidaBala.x * SalidaBala.x) + Mathf.Sqrt(SalidaBala.y * SalidaBala.y));
        GetComponent<Rigidbody>().AddForce(SalidaBala * 2500.0f);

    }

    private void OnCollisionEnter2D(Collision collision)
    {
        Destroy(gameObject);
    }

}
