using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP : MonoBehaviour {

	private LevelManagment boss;

        private void Start()
    {
        boss = FindObjectOfType<LevelManagment>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "humana")
        {
            boss.setCP(GetComponent<Transform>());
            Destroy(this);
        }
    }
}
