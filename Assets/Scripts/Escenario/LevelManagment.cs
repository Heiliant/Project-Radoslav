using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelManagment : MonoBehaviour {
    public GameObject player;
    public GameObject playerPrefab;
    private Transform lastCP;
    public Transform spawn;
    public Text Relevant;
    private float counter=0;
    private void Start()
    {
        lastCP = spawn;
        Relevant.text = "";
    }

    public void setCP(Transform CP)
    {
        lastCP = CP;
    }

    private void Update()
    {
        if (player.GetComponent<CambioFormas>().currentHP == 0)
        {
            player.SetActive(false);
            Relevant.text = "Has muerto";
            counter += Time.deltaTime;
            FindObjectOfType<ComportamientoCamara>().stickTo(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position);
            Relevant.color = new Color((counter / 3) * 184, 0, 0, counter / 3);
            if (counter >= 6)
            {        
                Relevant.text = "";
                Relevant.color = new Color(0, 0, 0, 0);
                player.SetActive(true);

                player.GetComponent<Transform>().position = lastCP.position;

                player.GetComponent<CambioFormas>().currentHP = 3;
                player.GetComponent<CambioFormas>().invulnerable = false;
                FindObjectOfType<ComportamientoCamara>().stopStick();
                counter = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag=="humana" || collision.tag=="demonio") {
        
            player.GetComponent<CambioFormas>().killPlayer();
        }
    }
}
