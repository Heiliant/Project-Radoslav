using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioFormas : MonoBehaviour {
    public GameObject Humana;       //Aqui meto los gameobjects de Huama y Demonio. Se podria hacer a través de GetComponentInChildren, per creo que asi                            
    public GameObject Demonio;
    public GameObject Particulillas;
    public bool demon = false;

    private KeyCode TRANSFORMACION;

    public Transform DetectorSuelo;
    public float radDetSuelo = 0.40f;
    public bool enSuelo = false;
    public LayerMask suelo;

    public bool transf;
    public float timeToTrans;

    public void enableTransf()
    {
        TRANSFORMACION = KeyCode.E;
    }

    // Use this for initialization
    void Start () {
        Demonio.SetActive(false);
        Humana.SetActive(true);

        DetectorSuelo = GameObject.Find("DetectorSuelo").GetComponent<Transform>();

        transf = false;

        Particulillas.SetActive(true);
    }

    private void FixedUpdate()
    {
        Demonio.SetActive(demon); //Regulo que gameobject está activo, si Demonio o Humana.
        Humana.SetActive(!demon);

        FindObjectOfType<PlayerControl>().enabled = !demon;
        FindObjectOfType<DemonControl>().enabled = demon;

        enSuelo = Physics2D.OverlapCircle(new Vector2(DetectorSuelo.position.x, DetectorSuelo.position.y), radDetSuelo, suelo);
        if (!enSuelo)
            Debug.Log("Air");
        if (!demon)
        {
            FindObjectOfType<PlayerControl>().animacionHumana.SetBool("transformar", transf);
        }
        else
        {
            FindObjectOfType<DemonControl>().animacionDemon.SetBool("transformar", transf);
        }

    }
    // Update is called once per frame
    void Update () {
        //transformación

        if (Input.GetKeyDown(TRANSFORMACION) && !transf)
        {
            Debug.Log(GetComponent<PlayerControl>().getVelX());
                transf = true;
                StartCoroutine(TransformacionDemon(timeToTrans));
           
        }

        if (transf)
        {
            if (!demon)
            {
                Particulillas.GetComponent<Transform>().localPosition = new Vector2(Particulillas.transform.localPosition.x, 3.1f);
                Particulillas.GetComponent<Transform>().localScale = new Vector3(2.6025f, 2.6025f, 2.6025f);
            }
            else
            {
                Particulillas.GetComponent<Transform>().localPosition = new Vector2(Particulillas.transform.localPosition.x, 4.4f);
                Particulillas.GetComponent<Transform>().localScale = new Vector3(3.386891f, 3.386891f, 3.386891f);
            }
            //  Particulillas.SetActive(true);
            Invoke("doEmit", 0f);
        }
        //else
        //    Particulillas.SetActive(false);
    }

    IEnumerator TransformacionDemon(float t)
    {
        yield return new WaitForSeconds(t);
        transf = false;
        demon = !demon;
    }

    void doEmit()
    {
        ParticleSystem.EmitParams emisionArg = new ParticleSystem.EmitParams();
        if (!demon)
            emisionArg.startColor = Color.black;
        else
            emisionArg.startColor = Color.white;
        emisionArg.startSize = 5f;
        emisionArg.startLifetime = 0.76f;
        Particulillas.GetComponent<ParticleSystem>().Emit(emisionArg, 7);
    }
}
