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
    private float regularSizeCam=20;
    private float regularYCam;
    public bool startMoveB = false;
    public bool startMoveR = false;
    private float counter = 0;
    private bool checker = false;

    private Vector2 sticker;
    private bool stickMode=false;

    public bool quake;

    public void bossSize()
    {
        startMoveB = true;
        startMoveR = false;
    }
    public void regularSize()
    {
        startMoveR = true;
        startMoveB = false;
    }

    public void stickTo (Vector2 a) //stickTo un vector2
    {
        sticker = a;
        Debug.Log(a);
        stickMode = true;
    }
    
    public void stopStick() //stop stick para volver al PJ.
    {
        stickMode = false;
    }

    public void tremble(float a)
    {
        if (!checker)
        {
            GetComponent<Transform>().Translate(a, 0, 0);
            checker = true;
        }
        else
        {
            GetComponent<Transform>().Translate(-a, 0, 0);
            checker = false;
        }
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!stickMode)
        {
            GetComponent<Transform>().position =
                new Vector3(player.GetComponent<Transform>().position.x + Input.mousePosition.x /
                           (Screen.currentResolution.width / PotestadDelMouse) + desplazadoX,
                            player.GetComponent<Transform>().position.y + Input.mousePosition.y /
                           (Screen.currentResolution.height / PotestadDelMouse) + desplazadoY,
                            GetComponent<Transform>().position.z);
        }
        else
        {
            GetComponent<Transform>().position = new Vector3(sticker.x, sticker.y, GetComponent<Transform>().position.z);
        }
        if (quake)
            this.tremble(0.4f);
    }

    private void FixedUpdate()
    {
        if (startMoveB)
        {
            if (GetComponent<Camera>().orthographicSize < bossSizeCam)
                GetComponent<Camera>().orthographicSize+=0.13f;
            if ((GetComponent<Transform>().position.y - player.GetComponent<Transform>().position.y) < bossYCam)
                desplazadoY += 0.2f;
            if (GetComponent<Camera>().orthographicSize > bossSizeCam && (GetComponent<Transform>().position.y - player.GetComponent<Transform>().position.y) > bossYCam)
            {
                startMoveB = false;
            }
        }
        if (startMoveR)
        {
            if (GetComponent<Camera>().orthographicSize > regularSizeCam)
                GetComponent<Camera>().orthographicSize -= 0.13f;
            if ((GetComponent<Transform>().position.y - player.GetComponent<Transform>().position.y) > regularYCam)
                desplazadoY -= 0.2f;
            if (GetComponent<Camera>().orthographicSize < regularSizeCam && (GetComponent<Transform>().position.y - player.GetComponent<Transform>().position.y) < regularYCam)
            {
                startMoveR = false;
            }
        }
        
        
            GameObject.Find("fondo luna").GetComponent<Transform>().localScale =
                new Vector3((GetComponent<Camera>().orthographicSize / 20) * 1.05f, (GetComponent<Camera>().orthographicSize / 20), 1);
        
    }
}