using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagment : MonoBehaviour {
    public GameObject player;
    private Transform lastCP;

    public void setCP(Vector3 CP)
    {
        lastCP.position = CP;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.transform.position = lastCP.position;
        player.GetComponent<PlayerControl>().attackPlayer();
    }
}
