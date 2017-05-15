using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoSol : MonoBehaviour {
    public enum Estado 
    {   nullemod,
        barrido, 
        lluvia,
        embestida,
        embestida2,
        embestida3,
        embestida4
    }
    public Transform[]  embestida;
    public Transform PJpos;
    public Transform recoverPos;
    private Vector2 PJtarget;
    public GameObject aura;
    public Estado modo;
    public float embestidaSpeed;
    private int currentTarget;
    private float counter = 0;
    public float timeToTargetPJ;
    public GameObject Disparo;
    public Transform[] ShotSpawnLat;
    private bool localshot = false;
    private void goTo(Vector2 dest, float speed) 
    {
            Vector2 direction = new Vector2(dest.x - GetComponent<Transform>().position.x, dest.y - GetComponent<Transform>().position.y);
            float localmod = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
            direction = new Vector2(direction.x / localmod, direction.y / localmod);
            transform.Translate(direction.x * speed, direction.y * speed, 0f);
    } //Dirige el sol hacia el punto dest a la velocidad speed.

    private bool imThere(Vector2 dest) //devuelve 1 si aún NO ha llegado, y 0 si SI que ha llegado.
    {
        if (transform.position.x > dest.x + 2f || transform.position.x < dest.x - 2f ||
            transform.position.y > dest.y + 2f || transform.position.y < dest.y - 2f)
            return false;
        else
            return true;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        switch (modo)
        {
            case Estado.embestida: //decide a que spot dirigirse
                currentTarget = Random.Range(0, 2);
                modo = Estado.embestida2;
                break;

            case Estado.embestida2: //avanza hacia el spot
                if (!imThere(embestida[currentTarget].position))
                    goTo(embestida[currentTarget].position, embestidaSpeed);
                else
                {
                    modo = Estado.embestida3;
                    FindObjectOfType<MovimientoAura>().vel = 110;

                }
                break;

            case Estado.embestida3: //cuando está en el spot, se queda allí un rato (timeToTargetPJ) y apunta al PJ. 
                counter += Time.deltaTime;
                if (counter < timeToTargetPJ)
                {
                    PJtarget = PJpos.position;
                    float localParameterTime = counter / timeToTargetPJ;
                    aura.GetComponent<SpriteRenderer>().color = new Color(1f - localParameterTime/5, 1f-localParameterTime, 1f-localParameterTime, 1f);
                }
                else
                {
                    GameObject.Find("pupila I").GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
                    GameObject.Find("pupila D").GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
                    if (counter >= timeToTargetPJ * 1.4f)
                    {
                        modo = Estado.embestida4;
                        embestidaSpeed = 3.2f;
                        counter = 0;
                    }
                }
                break;

            case Estado.embestida4:
                
                if (!imThere(PJtarget) && counter==0)
                {
                    goTo(PJtarget, embestidaSpeed);
                    localshot = true;
                }
                else
                {
                    if (counter < 4f)
                    {
                        counter += Time.deltaTime;
                        if (localshot)
                        {
                            StartCoroutine(ShotEmUp(0, Disparo, ShotSpawnLat[0].position, new Quaternion(-90, 90, 0, 0)));
                            StartCoroutine(ShotEmUp(1.5f, Disparo, ShotSpawnLat[1].position, new Quaternion(-90, 90, 0, 0)));
                            StartCoroutine(ShotEmUp(3f, Disparo, ShotSpawnLat[2].position, new Quaternion(-90, 90, 0, 0)));

                            StartCoroutine(ShotEmUp(0, Disparo, ShotSpawnLat[3].position, new Quaternion(90, 90, 0, 0)));
                            StartCoroutine(ShotEmUp(1.5f, Disparo, ShotSpawnLat[4].position, new Quaternion(90, 90, 0, 0)));
                            StartCoroutine(ShotEmUp(3f, Disparo, ShotSpawnLat[5].position, new Quaternion(90, 90, 0, 0)));
                            localshot = false;
                        }
                        
                        if (counter <= 0.4f)
                        {
                            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>().tremble(0.15f);
                        }
                        else if(counter >3f)
                        {
                            if (!imThere(new Vector2(GetComponent<Transform>().position.x, recoverPos.position.y)))
                                goTo(new Vector2(GetComponent<Transform>().position.x, recoverPos.position.y), 2f);
                        }
                    }
                    else
                    {
                        StartCoroutine(SetState(9f, Estado.nullemod));
                        counter = 0;
                    }
                }
                break;

            case Estado.lluvia:
                break;
            case Estado.barrido:
                break;
            case Estado.nullemod:
                embestidaSpeed = 0.2f;
                FindObjectOfType<MovimientoAura>().vel = 50;
                GameObject.Find("pupila I").GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
                GameObject.Find("pupila D").GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
                if(aura.GetComponent<SpriteRenderer>().color != new Color(1f, 1f, 1f, 1f))
                {
                    counter += Time.deltaTime;
                    aura.GetComponent<SpriteRenderer>().color = new Color(aura.GetComponent<SpriteRenderer>().color.r+counter/15,
                        aura.GetComponent<SpriteRenderer>().color.g + counter/3, aura.GetComponent<SpriteRenderer>().color.b + counter/3, 1f);
                }
                if (!imThere(recoverPos.position))
                    goTo(recoverPos.position, embestidaSpeed);
                break;
        }
	}
    IEnumerator SetState(float a, Estado s)
    {
        yield return new WaitForSeconds(a);
        modo = s;
    }

    IEnumerator ShotEmUp(float a, GameObject go, Vector3 pos, Quaternion rot)
    {
        yield return new WaitForSeconds(a);
        Instantiate(go, pos, rot);
    }
}
