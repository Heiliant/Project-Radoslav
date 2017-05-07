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
            if (transf)
                FindObjectOfType<PlayerControl>().stayQuiet(true);
            else
                FindObjectOfType<PlayerControl>().stayQuiet(false);
        }
        else
        {
            FindObjectOfType<DemonControl>().animacionDemon.SetBool("transformar", transf);
            if (transf)
                FindObjectOfType<DemonControl>().stayQuiet(true);
            else
                FindObjectOfType<DemonControl>().stayQuiet(false);
        }

    }
    // Update is called once per frame
    void Update () {
        //transformación

        if (Input.GetKeyDown(TRANSFORMACION))
        {
            Debug.Log(GetComponent<PlayerControl>().getVelX());
            if ((!demon && (GetComponent<PlayerControl>().getVelX() > -0.1f && GetComponent<PlayerControl>().getVelX() < 0.1f) && enSuelo)
               || (demon && (GetComponent<Rigidbody2D>().velocity.x < 0.1f && GetComponent<Rigidbody2D>().velocity.x > -0.1f) && enSuelo))
            {
                transf = true;
                StartCoroutine(TransformacionDemon(timeToTrans));
            }
            
        }

        if (transf)
        {
            int localDir;
            if (!demon)
                localDir = 1;
            else
                localDir = -1;

            Particulillas.SetActive(true);
            Particulillas.transform.Translate(0f, 0.03f*localDir, 0f);
        }
        else
            Particulillas.SetActive(false);
    }

    IEnumerator TransformacionDemon(float t)
    {
        yield return new WaitForSeconds(t);
        transf = false;
        demon = !demon;
        if (demon)
            Particulillas.transform.Translate(0f, 4f, 0f);
        else
            Particulillas.transform.Translate(0f, -4f, 0f);
    }
}
