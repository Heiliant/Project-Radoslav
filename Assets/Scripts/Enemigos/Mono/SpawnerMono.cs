using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMono : MonoBehaviour {
    public GameObject mono;
    public ComportamientoCamara cam;
    public GameObject poderinfo;
    // Use this for initialization
    void Start()
    {
        GetComponent<Transform>().position = new Vector3(139.9f, 46.4f, 20f);
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ComportamientoCamara>();
        StartCoroutine(wait(1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.tag.Equals("humana"))
        {
            FindObjectOfType<LevelManagment>().boss=Instantiate(mono, GetComponent<Transform>().position, new Quaternion(0, 0, 0, 1));
            FindObjectOfType<LevelManagment>().boss.GetComponent<ComportamientoMono>().poderinfo = poderinfo;
            cam.bossSize();
            GameObject.FindObjectOfType<ComportamientoMono>().startFight();
            GetComponent<BoxCollider2D>().enabled=(false);
        }
    }
    IEnumerator wait(float a)
    {
        yield return new WaitForSeconds(a);
        gameObject.SetActive(GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().mono);
    }
}
