using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoSol : MonoBehaviour {
    public enum Estado 
    {   nullemod,
        laser, 
        laser2,
        lluvia,
        lluvia2,
        embestida,
        embestida2, 
        embestida3,
        embestida4
    }
    public Transform[] Platform;
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
    public Transform LluviaSolSpot;
    public GameObject[] ShotSpawnersLluvia;
    public GameObject FireRing;
    public GameObject[] Laser;
    private Vector3[] laserray;
    private GameObject localLaser;

    private bool localCheck=false;
    private int laserOrigin;
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
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>().
            stickTo(GameObject.Find("BossSol").GetComponent<Transform>().position);

        laserray = new Vector3[2];
        laserOrigin = Random.Range(0, 2);
        laserray[0] = GetComponent<Transform>().position;
        laserray[1] = Platform[laserOrigin].position;
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
                if (!imThere(LluviaSolSpot.position))
                {
                    goTo(LluviaSolSpot.position, 0.2f);
                }
                else
                {
                    modo = Estado.lluvia2;
                    counter = 0;
                }
                break;
            case Estado.lluvia2:
                
                if (!ShotSpawnersLluvia[0].activeSelf)
                {
                    foreach (GameObject a in ShotSpawnersLluvia)
                        a.SetActive(true);
                }

                counter += Time.deltaTime;
                if (counter >= 1f){
                    aura.GetComponent<MovimientoAura>().maxSize = 1.4f;
                    if (!aura.GetComponent<MovimientoAura>().decreaseScale)
                        aura.GetComponent<MovimientoAura>().scaleSpeed = 2f;
                    else
                        aura.GetComponent<MovimientoAura>().scaleSpeed = 0.4f;

                    if (counter >= 3.5f)
                    {
                        counter = 0;
                        int localRand = Random.Range(-90, 91);
                        aura.GetComponent<MovimientoAura>().scaleSpeed = 0.1f;
                        aura.GetComponent<MovimientoAura>().resetAuraScale = true;
                        GameObject localRing=Instantiate(FireRing, GetComponent<Transform>().position,GetComponent<Transform>().rotation);
                        localRing.GetComponent<FireRingmov>().rotSpeed = 0.3f*Random.Range(-1, 2);

                        aura.GetComponent<MovimientoAura>().maxSize = 1.1f;
                        aura.GetComponent<MovimientoAura>().scaleSpeed = 0.1f;
                    }
                }
                else
                {

                }
                break;
            case Estado.laser:
                if (!imThere(LluviaSolSpot.position))
                    goTo(LluviaSolSpot.position, embestidaSpeed);
                else
                {
                    modo = Estado.laser2;
                }
                break;
            case Estado.laser2:

                if (!localCheck)
                {
                    localCheck = true;
                    localLaser = Instantiate(Laser[Random.Range(0, 2)], GetComponent<Transform>().position,
                        new Quaternion(0, 0, 0, 0));
                    localLaser.GetComponent<LineRenderer>().SetPosition(0, GetComponent<Transform>().position);
                    localLaser.GetComponent<LineRenderer>().SetPosition(1, Platform[laserOrigin].position);
                }
                if (!localLaser.GetComponent<ComportamientoLaser>().imThere(Platform[(laserOrigin+1)%2].position))
                    localLaser.GetComponent<ComportamientoLaser>().goTo(Platform[(laserOrigin+1)%2].position, 0.009f);
                Debug.DrawLine(Platform[0].position, Platform[1].position, Color.black);

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
                foreach (GameObject z in ShotSpawnersLluvia)
                {
                    z.SetActive(false);
                }
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
