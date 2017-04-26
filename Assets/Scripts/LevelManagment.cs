using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagment : MonoBehaviour {
    public GameObject player;
    private Transform lastCP;

    public void setCP(Transform CP)
    {
        lastCP = CP;
    }

    private void Update()
    {
        if (player.GetComponent<PlayerControl>().currentHP == 0)
        {
            player.transform.position = lastCP.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.transform.position = lastCP.position;
        player.GetComponent<PlayerControl>().killPlayer();
    }
}
