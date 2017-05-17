using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoLaser : MonoBehaviour {





    public void goTo(Vector2 dest, float speed)
    {
        Vector2 direction = new Vector2(dest.x - GetComponent<LineRenderer>().GetPosition(1).x, dest.y - GetComponent<LineRenderer>().GetPosition(1).y);
        float localmod = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
        direction = new Vector2(direction.x / localmod, direction.y / localmod);
        Vector3 localVec = new Vector2(direction.x + GetComponent<LineRenderer>().GetPosition(1).x, direction.y + GetComponent<LineRenderer>().GetPosition(1).y);
        GetComponent<LineRenderer>().SetPosition(1, new Vector3(localVec.x, localVec.y, GetComponent<LineRenderer>().GetPosition(1).z));
    } //Dirige el sol hacia el punto dest a la velocidad speed.

    public bool imThere(Vector2 dest) //devuelve 1 si aún NO ha llegado, y 0 si SI que ha llegado.
    {
        Debug.Log("me"+GetComponent<LineRenderer>().GetPosition(1));
        Debug.Log("dest"+dest);
        return (GetComponent<LineRenderer>().GetPosition(1).x == dest.x) && (GetComponent<LineRenderer>().GetPosition(1).y == dest.y);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
