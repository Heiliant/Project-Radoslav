using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour {
    public float speed;
    public int destinyScene;
    public bool salida;
    private bool autodestroy;
    private float counter;
    public float deathTime;
    // Update is called once per frame
    private void Start()
    {
        counter=0.001f;
        autodestroy = false;
    }
    void Update () {
        GetComponent<Transform>().Rotate(0, 0, speed);
        if (autodestroy)
        {
            counter += Time.deltaTime;
            GetComponent<Transform>().localScale = new Vector2(GetComponent<Transform>().localScale.x - GetComponent<Transform>().localScale.x*(counter/deathTime),
                GetComponent<Transform>().localScale.y - GetComponent<Transform>().localScale.y*(counter / deathTime));

            if (counter >= deathTime)
                Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (salida)
        {
            SceneManager.LoadScene(destinyScene, LoadSceneMode.Single);
            PlayerPrefs.SetInt("jumps", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().getAmountOfJumps());
            PlayerPrefs.SetInt("transf", GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().getTransf().ToString()!=null? 1:0);
            PlayerPrefs.SetInt("disparo", GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().disparoSkill ? 1 : 0);
            PlayerPrefs.SetInt("stage", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt("bossMono", GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().mono ? 1 : 0);
            PlayerPrefs.SetInt("bossSol", GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().sol ? 1 : 0);
            PlayerPrefs.SetInt("bossDragon", GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().dragon ? 1 : 0);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().setAmountOfJumps(PlayerPrefs.GetInt("jumps", 0));
            if (PlayerPrefs.GetInt("transf", 0)==1)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().enableTransf();
            }
            else
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().setTransfKey(KeyCode.None);
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().disparoSkill = (PlayerPrefs.GetInt("disparo", 0) == 1 ? true : false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().mono = PlayerPrefs.GetInt("bossMono", 1) == 1 ? true : false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().sol = PlayerPrefs.GetInt("bossSol", 1) == 1 ? true : false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<CambioFormas>().dragon = PlayerPrefs.GetInt("bossDragon", 1) == 1 ? true : false;
            autodestroy = true;
        }
    }
}
