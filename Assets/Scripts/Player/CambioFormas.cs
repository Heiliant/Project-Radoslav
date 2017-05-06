using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioFormas : MonoBehaviour {
    public GameObject Humana;       //Aqui meto los gameobjects de Huama y Demonio. Se podria hacer a través de GetComponentInChildren, per creo que asi                            
    public GameObject Demonio;
    private bool demon = false;

    private KeyCode TRANSFORMACION = KeyCode.E;

    public Transform DetectorSuelo;
    public float radDetSuelo = 0.40f;
    public bool enSuelo = false;
    public LayerMask suelo;

    // Use this for initialization
    void Start () {
        Demonio.SetActive(false);
        Humana.SetActive(true);

        DetectorSuelo = GameObject.Find("DetectorSuelo").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Demonio.SetActive(demon); //Regulo que gameobject está activo, si Demonio o Humana.
        Humana.SetActive(!demon);

        FindObjectOfType<PlayerControl>().enabled = !demon;
        FindObjectOfType<DemonControl>().enabled = demon;

        enSuelo = Physics2D.OverlapCircle(new Vector2(DetectorSuelo.position.x, DetectorSuelo.position.y), radDetSuelo);

    }
    // Update is called once per frame
    void Update () {
        //transformación

        if (Input.GetKeyDown(TRANSFORMACION))
        {
            if (demon)
                demon = false;
            else
                demon = true;
        }
    }
}
