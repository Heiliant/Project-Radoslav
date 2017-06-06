using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonDamage : MonoBehaviour {
    public float dragonHP = 1;
    public GameObject BossHP;
    public GameObject BossHPActual;
    public GameObject cabeza;
    public float counter = 0;
    public GameObject dragon;
    public GameObject portal;

    private void Start()
    {
        BossHP.SetActive(false);
        BossHPActual.SetActive(false);
        StartCoroutine(wait(1));
    }
    private void Update()
    {   
        BossHPActual.GetComponent<RectTransform>().localScale = new Vector2((dragonHP / 550f), BossHPActual.GetComponent<RectTransform>().localScale.y);
        if (dragonHP <= 0)
        {
            BossHP.SetActive(false);
            BossHPActual.SetActive(false);
            StartCoroutine(AutoDestroy(5));
            FindObjectOfType<ComportamientoCamara>().breakFree(true);
            FindObjectOfType<ComportamientoCamara>().startMoveB=true;
            FindObjectOfType<ComportamientoCamara>().startMoveR = false;
            foreach(floorSpawner a in FindObjectsOfType<floorSpawner>())
            {
                a.gameObject.SetActive(false);
            }
            gameObject.GetComponentInParent<ComportamientoDragonMovimiento>().modo = ComportamientoDragonMovimiento.estado.nulo;
            counter += Time.deltaTime;
            foreach(SpriteRenderer a in dragon.GetComponentsInChildren<SpriteRenderer>())
            {
                a.color = new Color(1, 1, 1, 1-counter/5);
            }
        }
        float localColor = dragonHP / 550f;
        cabeza.GetComponent<SpriteRenderer>().color = new Color(1, localColor, localColor);
    }

    public void startBar()
    {
        BossHP.SetActive(true);
        BossHPActual.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("demonshot"))
        {
            collision.GetComponent<demonShotBehaviour>().explode();
            --dragonHP;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator AutoDestroy(float a)
    {
        yield return new WaitForSeconds(a);
        GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().dragon = false;
        GameObject localGO=Instantiate(portal, GetComponent<Transform>().position, new Quaternion(0, 0, 0, 1));
        localGO.GetComponent<portal>().destinyScene = 7;
        localGO.GetComponent<portal>().salida = true;
        localGO.GetComponent<portal>().deathTime = 5;
        localGO.GetComponent<portal>().speed = 3;
        
        Destroy(dragon.gameObject);
    }

    IEnumerator wait(float a)
    {
        yield return new WaitForSeconds(a);
        gameObject.SetActive(GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().dragon);
    }
}
