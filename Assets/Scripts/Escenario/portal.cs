using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour {
    public float speed;
    public int destinyScene;
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().Rotate(0, 0, speed);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(destinyScene, LoadSceneMode.Single);
    }
}
