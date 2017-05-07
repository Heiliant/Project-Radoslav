using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioFormas : MonoBehaviour {
    public GameObject Humana;       //Aqui meto los gameobjects de Huama y Demonio. Se podria hacer a través de GetComponentInChildren, per creo que asi                            
    public GameObject Demonio;
    public GameObject Particulillas;
    public bool demon = false;

    private KeyCode TRANSFORMACION = KeyCode.E;

    public Transform DetectorSuelo;
    public float radDetSuelo = 0.40f;
    public bool enSuelo = false;
    public LayerMask suelo;

    public bool transf;
    public float timeToTrans;

    // Use this for initialization
    void Start () {
        Demonio.SetActive(false);
        Humana.SetActive(true);

        DetectorSuelo = GameObject.Find("DetectorSuelo").GetComponent<Transform>();

        transf = false;
    }

    private void FixedUpdate()
    {
        Demonio.SetActive(demon); //Regulo que gameobject está activo, si Demonio o Humana.
        Humana.SetActive(!demon);

        FindObjectOfType<PlayerControl>().enabled = !demon;
        FindObjectOfType<DemonControl>().enabled = demon;

        enSuelo = Physics2D.OverlapCircle(new Vector2(DetectorSuelo.position.x, DetectorSuelo.position.y), radDetSuelo, suelo);

        if (!demon)
        {
            FindObjectOfType<PlayerControl>().animacionHumana.SetBool("transformar", transf);
        }

    }
    // Update is called once per frame
    void Update () {
        //transformación

        if (Input.GetKeyDown(TRANSFORMACION))
        {
            transf = true;
            StartCoroutine(TransformacionDemon(timeToTrans));
            
        }

        if (transf)
            Particulillas.SetActive(true);
        else
            Particulillas.SetActive(false);
    }

    IEnumerator TransformacionDemon(float t)
    {
        yield return new WaitForSeconds(t);
        transf = false;
        demon = !demon;
        if (!demon)
        {
            Debug.Log(GameObject.FindGameObjectWithTag("muñeco").GetComponent<Transform>().rotation);
        }
    }
}
