using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoCamara : MonoBehaviour
{
    public GameObject player;
    public float PotestadDelMouse=10f;
    private float desplazadoX=0;
    private float desplazadoY=0;
    private float bossSizeCam = 28;
    private float bossYCam = 15;
    public bool startMove = false;

    public void bossSize()
    {
        startMove = true;
    }

    

    public void regularSize()
    {

    }
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = 
            new Vector3(player.GetComponent<Transform>().position.x + Input.mousePosition.x/
                       (Screen.currentResolution.width/PotestadDelMouse) + desplazadoX,
                        player.GetComponent<Transform>().position.y+ Input.mousePosition.y/
                       (Screen.currentResolution.height/PotestadDelMouse) + desplazadoY,
                        GetComponent<Transform>().position.z);

        
    }

    private void FixedUpdate()
    {
        if (startMove)
        {
            if (GetComponent<Camera>().orthographicSize < bossSizeCam)
                GetComponent<Camera>().orthographicSize+=0.13f;
            if ((GetComponent<Transform>().position.y - player.GetComponent<Transform>().position.y) < bossYCam)
                desplazadoY += 0.2f;
            if (GetComponent<Camera>().orthographicSize > bossSizeCam && (GetComponent<Transform>().position.y - player.GetComponent<Transform>().position.y) > bossYCam)
            {
                startMove = false;
                GameObject.FindObjectOfType<ComportamientoMono>().startFight();
            }
        }
        GameObject.Find("fondo luna").GetComponent<Transform>().localScale = 
            new Vector3 ((GetComponent<Camera>().orthographicSize / 20)* 1.05f, (GetComponent<Camera>().orthographicSize / 20), 1);
     
    }
}