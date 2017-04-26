using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagment : MonoBehaviour {
    public GameObject player;
    private Transform lastCP;
    public Transform spawn;

    private void Start()
    {
        lastCP = spawn;
    }

    public void setCP(Transform CP)
    {
        lastCP = CP;
    }

    private void Update()
    {
        if (player.GetComponent<PlayerControl>().currentHP == 0)
        {
            player.transform.position = lastCP.position;
            player.GetComponent<PlayerControl>().currentHP = 3;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag=="humana") {
            player.transform.position = lastCP.position;
            player.GetComponent<PlayerControl>().killPlayer();
        }
    }
}
