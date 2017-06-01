using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonDamage : MonoBehaviour {
    private float dragonHP = 1;
    public GameObject BossHP;
    public GameObject BossHPActual;

    private void Start()
    {
        BossHP.SetActive(false);
        BossHPActual.SetActive(false);
    }
    private void Update()
    {   
        BossHPActual.GetComponent<RectTransform>().localScale = new Vector2((dragonHP / 550f) * 4.7f, BossHPActual.GetComponent<RectTransform>().localScale.y);
        if (dragonHP <= 0)
        {
            BossHP.SetActive(false);
            BossHPActual.SetActive(false);
            StartCoroutine(AutoDestroy(5));
            FindObjectOfType<ComportamientoCamara>().breakFree(false);
            FindObjectOfType<ComportamientoCamara>().startMoveB = false;
            FindObjectOfType<ComportamientoCamara>().startMoveR = true;
            Destroy(FindObjectOfType<floorSpawner>().gameObject);
        }
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
        Destroy(gameObject);
    }
}
