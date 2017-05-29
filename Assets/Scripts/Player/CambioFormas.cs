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

    public int currentHP;
    private int lastHP;
    public float segunderoI = 0;
    public float RecoveryTime;  
    public bool invulnerable = false;
    private bool disparoSkill;

    public GameObject pauseMenu;

    public void attackPlayer(float a)
    {
        if (!invulnerable)
        {
            currentHP--;
            foreach (SpriteRenderer w in GetComponentsInChildren<SpriteRenderer>())
            {
                w.color = new Color(1, 0, 0, 1);
            }
        GetComponent<Rigidbody2D>().AddForce(new Vector2(FindObjectOfType<PlayerControl>().FuerzaSalto/2*a, FindObjectOfType<PlayerControl>().FuerzaSalto / 2));
        }
    }

    public void attackPlayer()
    {
        if (!invulnerable)
        {
            currentHP--;
            foreach (SpriteRenderer w in GetComponentsInChildren<SpriteRenderer>())
            {
                w.color = new Color(1, 0, 0, 1);
            }
        }
    }

    public void killPlayer()
    {
        currentHP = 0;
    }

    public void enableTransf()
    {
        TRANSFORMACION = KeyCode.E;
    }



    // Use this for initialization
    void Start () {
        disparoSkill = false;

        Demonio.SetActive(false);
        Humana.SetActive(true);

        DetectorSuelo = GameObject.Find("DetectorSuelo").GetComponent<Transform>();

        transf = false;

        Particulillas.SetActive(true);

        currentHP = 3;
        lastHP = currentHP;
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
            Invoke("doEmit", 0f);
        }

        if (lastHP != currentHP)
        {
            invulnerable = true;
        }
        else
        {
            foreach (SpriteRenderer w in GetComponentsInChildren<SpriteRenderer>())
            {
                w.color = new Color(1, 1, 1, 1);
            }
        }


        if (segunderoI >= RecoveryTime)
        {
            invulnerable = false;
            segunderoI = 0;
        }

        if (invulnerable)
        {
            segunderoI += Time.deltaTime;

            foreach (SpriteRenderer a in GetComponentsInChildren<SpriteRenderer>())
            {
                float localAlpha = segunderoI % 1;
                a.color = new Color(1f, 1f, 1f, localAlpha);
            }
        }

        lastHP = currentHP;

        Physics2D.Linecast(GetComponent<Transform>().position, Input.mousePosition);
        Debug.DrawLine(GetComponent<Transform>().position, Input.mousePosition, Color.magenta);
        if (Input.GetKey(KeyCode.Mouse1))
        {
            GetComponent<Transform>().position = Input.mousePosition;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = pauseMenu.activeSelf ? 1 : 0;
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            
        }

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
