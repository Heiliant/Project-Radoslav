using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoCamara : MonoBehaviour
{
    public GameObject player;
    private PlayerControl script;
    public float PotestadDelMouse=10f;
    // Use this for initialization
    void Start()
    {
        script = player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = 
            new Vector3(player.GetComponent<Transform>().position.x + Input.mousePosition.x/
                                                (Screen.currentResolution.width/PotestadDelMouse),
                        player.GetComponent<Transform>().position.y + Input.mousePosition.y/
                                                (Screen.currentResolution.height/PotestadDelMouse),
                        GetComponent<Transform>().position.z);
    }
}