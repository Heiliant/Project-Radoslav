using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoSol : MonoBehaviour {
    public enum Estado 
    {   sleep=-1,
        nullemod =0, //Al alternar entre modos es conveniente que al acabar un ciclo (embestida 1, 2, 3, 4), antes de ejecutar un nuevo estado,
        laser=1,  //se pase antes por el estado nullemod para resetear todas las componentes que se van usando de manera local. 
        laser2=2,
        lluvia=3,
        lluvia2=4,
        embestida=5,
        embestida2=6, 
        embestida3=7,
        embestida4=8,
        death=9,
        max=10
    }
    public Transform[] Platform;
    public Transform[]  embestida;
    public LayerMask capaCaraSol;
    public Transform PJpos;
    public Transform recoverPos;
    private Vector2 PJtarget;
    public GameObject aura;
    public Estado modo;
    public float embestidaSpeedOrigin;
    private float embestidaSpeed;
    public float embestidatoPJSpeed;
    private int currentTarget;
    public float counter = 0;
    public float timeToTargetPJ;
    public GameObject Disparo;
    public float EmbestidaShotsAmount;
    private bool localshot = false;
    public Transform LluviaSolSpot;
    public GameObject[] ShotSpawnersLluvia;
    public GameObject FireRing;
    public GameObject[] Laser;
    public LayerMask PJ_Layer;
    private Vector3[] laserray;
    private GameObject localLaser;
    private int laserDecision;

    public GameObject BossHP;
    public GameObject BossHPActual;
    public float HP = 100;

    public GameObject poder;

    private Estado lastmode; 
    private bool localCheck=false;
    private bool localCheckHit = false;
    private int laserOrigin;
    private void goTo(Vector2 dest, float speed) 
    {
            Vector2 direction = new Vector2(dest.x - GetComponent<Transform>().position.x, dest.y - GetComponent<Transform>().position.y);
            float localmod = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
            direction = new Vector2(direction.x / localmod, direction.y / localmod);
            transform.Translate(direction.x * speed, direction.y * speed, 0f);
    } //Dirige el sol hacia el punto dest a la velocidad speed.
    private bool comprobaciondeath = false;
    public GameObject PoderInfo;
    public GameObject portal;
    public Transform portalSpot;
    public int nextScene;
    private bool imThere(Vector2 dest) //devuelve 1 si aún NO ha llegado, y 0 si SI que ha llegado.
    {
        return (transform.position.x < dest.x + 1.2f && transform.position.x > dest.x - 1.2f &&
            transform.position.y < dest.y + 1.2f && transform.position.y > dest.y - 1.2f);
    }

    public void dmgSun()
    {
        HP -= 10;

        if(modo==Estado.embestida4 || modo == Estado.lluvia2 || modo == Estado.laser2 || modo == Estado.embestida3)
        modo = Estado.nullemod;
    }

	// Use this for initialization
	void Start () {/*
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>().
            stickTo(GameObject.Find("BossSol").GetComponent<Transform>().position);*/

        modo = Estado.sleep;
        laserray = new Vector3[2];
        laserOrigin = Random.Range(0, 2);
        laserray[0] = GetComponent<Transform>().position;
        laserray[1] = Platform[laserOrigin].position;

        
    }

    private void FixedUpdate()
    {
        if(HP>=0)
            BossHPActual.GetComponent<RectTransform>().localScale = new Vector2((HP / 100f), BossHPActual.GetComponent<RectTransform>().localScale.y);
        else
            BossHPActual.GetComponent<RectTransform>().localScale = new Vector2(0, BossHPActual.GetComponent<RectTransform>().localScale.y);
        if (HP <= 0 && !comprobaciondeath )
        {
            FindObjectOfType<ComportamientoPilon>().HP = 0;
            StartCoroutine(Autodestroy(5f));
            modo = Estado.nullemod;
            comprobaciondeath = true;
        }
    }

    // Update is called once per frame
    void Update () {
        switch (modo)
        {
            case Estado.embestida: //decide a que spot dirigirse
                currentTarget = Random.Range(0, 3);
                counter = 0;
                modo = Estado.embestida2;
                break;

            case Estado.embestida2: //avanza hacia el spot
                if (!imThere(embestida[currentTarget].position))
                    goTo(embestida[currentTarget].position, embestidaSpeed);
                else
                {
                    modo = Estado.embestida3;
                    FindObjectOfType<MovimientoAura>().vel = 180;

                }
                break;

            case Estado.embestida3: //cuando está en el spot, se queda allí un rato (timeToTargetPJ) y apunta al PJ. 
                counter += Time.deltaTime;
                if (counter < timeToTargetPJ)
                {
                    PJtarget = PJpos.position;
                    float localParameterTime = counter / timeToTargetPJ;
                    aura.GetComponent<SpriteRenderer>().color = new Color(1f - localParameterTime / 5, 1f - localParameterTime, 1f - localParameterTime, 1f);
                }
                else
                {
                    GameObject.Find("pupila I").GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
                    GameObject.Find("pupila D").GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
                    if (counter >= timeToTargetPJ * 1.4f)
                    {
                        modo = Estado.embestida4;
                        embestidaSpeed = embestidatoPJSpeed;
                        counter = 0;
                    }
                }
                break;

            case Estado.embestida4:

                if (!imThere(PJtarget) && counter == 0)
                {
                    goTo(PJtarget, embestidaSpeed);
                    localshot = true;
                    localCheckHit = true;
                }
                else
                {
                    lastmode = Estado.embestida;
                    if (counter < 3f)
                    {
                        counter += Time.deltaTime;
                        GameObject.Find("Centro").GetComponent<SpriteRenderer>().color = new Color(1, 0.1f + counter / 3, 0.1f + counter / 3);
                        if (localshot)
                        {
                            for (int i = 1; i <= 360; ++i)
                            {
                                float localParam = (i / 90f);
                                RaycastHit2D localHit;
                                float localY = GetComponent<Transform>().position.y;
                                float localX = GetComponent<Transform>().position.x;
                                if (localParam <= 1)
                                {
                                    localY += (1f - localParam) * 20;
                                    localX += -localParam * 20;
                                    localHit = Physics2D.Linecast(GetComponent<Transform>().position, new Vector2(localX, localY), capaCaraSol);
                                }
                                else if (localParam <= 2)
                                {
                                    localParam--;
                                    localY += -localParam * 20;
                                    localX += (-1f + localParam) * 20;
                                    localParam++;
                                    localHit = Physics2D.Linecast(GetComponent<Transform>().position, new Vector2(localX, localY), capaCaraSol);
                                }
                                else if (localParam <= 3)
                                {
                                    localParam -= 2;
                                    localY += (-1f + localParam) * 20;
                                    localX += localParam * 20;
                                    localParam += 2;
                                    localHit = Physics2D.Linecast(GetComponent<Transform>().position, new Vector2(localX, localY), capaCaraSol);
                                }
                                else
                                {
                                    localParam -= 3;
                                    localX += (1f - localParam) * 20;
                                    localY += localParam * 20;
                                    localParam += 3;
                                    localHit = Physics2D.Linecast(GetComponent<Transform>().position, new Vector2(localX, localY), capaCaraSol);
                                }

                                if (i % (360 / EmbestidaShotsAmount) == 0)
                                {
                                    Debug.DrawLine(GetComponent<Transform>().position, new Vector2(localX, localY));
                                    StartCoroutine(ShotEmUp((i / 360f), Disparo, localHit.point, new Quaternion(0, 0, 0, 0), true));

                                }
                            }
                            localshot = false;

                        }

                        if (counter <= 0.4f)
                        {
                            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>().tremble(0.15f);
                        }
                    }
                    else
                    {
                        GameObject.Find("pupila I").GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
                        GameObject.Find("pupila D").GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
                        StartCoroutine(SetState(0f, Estado.embestida));
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
                    aura.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                    modo = Estado.lluvia2;
                    counter = 0;
                }
                break;
            case Estado.lluvia2:

                counter += Time.deltaTime;
                lastmode = Estado.lluvia;
                if (!ShotSpawnersLluvia[0].activeSelf)
                {
                    foreach (GameObject a in ShotSpawnersLluvia)
                        a.SetActive(true);
                }

                if (counter >= 1f)
                {
                    aura.GetComponent<MovimientoAura>().maxSize = 1.4f;
                    aura.GetComponent<MovimientoAura>().minSize = 0.9f;
                    if (!aura.GetComponent<MovimientoAura>().decreaseScale)
                        aura.GetComponent<MovimientoAura>().scaleSpeed = 2f;
                    else
                        aura.GetComponent<MovimientoAura>().scaleSpeed = 0.4f;

                    if (counter >= 7f)
                    {
                        counter = 0;
                        aura.GetComponent<MovimientoAura>().scaleSpeed = 0.1f;
                        aura.GetComponent<MovimientoAura>().resetAuraScale = true;
                        GameObject localRing = Instantiate(FireRing, GetComponent<Transform>().position, GetComponent<Transform>().rotation);
                        localRing.GetComponent<Transform>().Rotate(0, 0, Random.Range(0f, 360f));

                        aura.GetComponent<MovimientoAura>().maxSize = 1.1f;
                        aura.GetComponent<MovimientoAura>().scaleSpeed = 0.1f;
                    }
                }
                break;
            case Estado.laser:
                if (!imThere(LluviaSolSpot.position))
                    goTo(LluviaSolSpot.position, embestidaSpeed); //el sol se va a la posición lluviaSpot
                else
                {
                    laserDecision = Random.Range(0, 2);
                    counter = 0;
                    if (laserDecision == 1)
                        aura.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
                    else
                        aura.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1);
                    modo = Estado.laser2;
                }
                break;
            case Estado.laser2:
                counter += Time.deltaTime;
                lastmode = Estado.laser;
                if (!localCheck && counter >= 2)
                {
                    localCheck = true;
                    localLaser = Instantiate(Laser[laserDecision], GetComponent<Transform>().position, //aquí instancia un solo laser
                        new Quaternion(0, 0, 0, 0));            //elige aleatoriamente entre los 2 posibles láseres y entre los dos extremos de la
                    localLaser.GetComponent<LineRenderer>().SetPosition(0, GetComponent<Transform>().position);//plataforma como destino del láser
                    localLaser.GetComponent<LineRenderer>().SetPosition(1, Platform[laserOrigin].position);
                }



                if (counter >= 2.5f && localLaser != null)
                { //a partir de los 2 segundos, el láser empieza a avanzar hacia el otro extremo de la platform
                    RaycastHit2D laserImpacto = Physics2D.Linecast(GetComponent<Transform>().position, localLaser.GetComponent<LineRenderer>().GetPosition(1), PJ_Layer);
                    if (!localLaser.GetComponent<ComportamientoLaser>().imThere(Platform[(laserOrigin + 1) % 2].position))
                    {
                        localLaser.GetComponent<ComportamientoLaser>().goTo(Platform[(laserOrigin + 1) % 2].position, 1f);
                    }
                    else
                    {
                        StartCoroutine(SetState(2, Estado.nullemod)); //cuando llega, se espera 2 segundos y el láser desaparece
                        localCheck = true;
                    }
                    try
                    {
                        if (laserImpacto.collider.tag == ("humana") && localLaser.name.Equals("LaserHum(Clone)")) //si el láser rojo detecta a la humana, le hace daño
                        {
                            if (laserOrigin == 0)
                                FindObjectOfType<CambioFormas>().attackPlayer(-1);
                            else
                                FindObjectOfType<CambioFormas>().attackPlayer(1);
                        }
                        else if (laserImpacto.collider.tag == ("demonio") && localLaser.name.Equals("LaserDem(Clone)")) //si el láser verde detecta al demon, le hace daño
                        {
                            if (laserOrigin == 0)
                                FindObjectOfType<CambioFormas>().attackPlayer(-1);
                            else
                                FindObjectOfType<CambioFormas>().attackPlayer(1);
                        }
                    }
                    catch { };
                }
                break;
            case Estado.nullemod:
                aura.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                BossHP.SetActive(true);
                BossHPActual.SetActive(true);
                embestidaSpeed = embestidaSpeedOrigin;
                counter = 0;
                Destroy(localLaser);
                FindObjectOfType<MovimientoAura>().vel = 50;
                aura.GetComponent<SpriteRenderer>().color = new Color(aura.GetComponent<SpriteRenderer>().color.r + counter / 15,
                aura.GetComponent<SpriteRenderer>().color.g + counter / 3, aura.GetComponent<SpriteRenderer>().color.b + counter / 3, 1f);
                if (HP > 0)
                {
                    if (!imThere(recoverPos.position))
                    {
                        goTo(recoverPos.position, embestidaSpeed);
                        foreach (GameObject z in ShotSpawnersLluvia)
                        {
                            z.SetActive(false);
                        }
                        GameObject.Find("Centro").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                        localCheck = false;
                    }
                    else
                    {
                        switch (Random.Range(0, 3))
                        {
                            case 0:
                                modo = lastmode!=Estado.embestida? Estado.embestida : Estado.lluvia;
                                break;
                            case 1:
                                modo = lastmode!=Estado.lluvia? Estado.lluvia : Estado.laser;
                                break;
                            case 2:
                                modo = lastmode!=Estado.laser?Estado.laser:Estado.embestida;
                                break;
                        }
                    }
                    
                }
                else
                    modo = Estado.death;
                    break;
                case Estado.sleep:
                    BossHP.SetActive(false);
                    BossHPActual.SetActive(false);
                    break;
            case Estado.death:
                foreach (GameObject z in ShotSpawnersLluvia)
                {
                    z.SetActive(false);
                }
                counter += Time.deltaTime;
                foreach(SpriteRenderer a in GetComponentsInChildren<SpriteRenderer>())
                {
                    a.color = new Color(1, 1, 1, 1-counter/3);
                }
                break;
            }
        
	}
    IEnumerator SetState(float a, Estado s)
    {
        yield return new WaitForSeconds(a);
        if(modo==Estado.laser2)
            Destroy(localLaser);
        modo = s;
        counter=0;
    }

    IEnumerator ShotEmUp(float a, GameObject go, Vector3 pos, Quaternion rot, bool sunEdge)
    {
        yield return new WaitForSeconds(a);
        GameObject localGO=Instantiate(go, pos, rot);
        if (sunEdge)
            localGO.GetComponent<DisparosSol>().rotateFromSun();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (localCheckHit)
        {
            if (collision.tag == "humana" || collision.tag=="demonio")
            {
                int localrand = Random.Range(-1, 2);
                if (localrand == 0)
                    localrand++;
                FindObjectOfType<CambioFormas>().attackPlayer(localrand);
            }
            localCheckHit = false;
        }
    }
    IEnumerator Autodestroy(float a)
    {
        yield return new WaitForSeconds(a);
        BossHP.SetActive(false);
        BossHPActual.SetActive(false);
        GameObject local=Instantiate(poder, recoverPos.position-new Vector3(0, -10, 0), new Quaternion(0, 0, 0, 1));
        local.GetComponent<Transform>().Translate(0, 0, -4.4f);
        local.GetComponent<PoderSpawn>().tipo = PoderSpawn.boss.sol;
        local.GetComponent<PoderSpawn>().relevant = PoderInfo;
        GameObject wayOut=Instantiate(portal, portalSpot.position, portalSpot.rotation);
        wayOut.GetComponent<portal>().salida = true;
        wayOut.GetComponent<portal>().destinyScene = nextScene;
        Destroy(gameObject);
    }
}
