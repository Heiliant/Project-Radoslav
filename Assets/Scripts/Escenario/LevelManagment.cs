using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagment : MonoBehaviour {
    public GameObject player;
    private Transform lastCP;
    public Transform spawn;
    public Text Relevant;
    public float counter=0;
    public GameObject boss;
    public GameObject hisSpawner;
    public SpawnerMono script;
    public int[] bosslevels;
    private bool localBool;

    public AudioClip[] musica;
    public enum putoamo
    {
        mono, sol, dragon
    }
    public putoamo jefe;

    private void Start()
    {
        bosslevels = new int[2];
        bosslevels[0] = 4;
        bosslevels[1] = 7;
        localBool = false;
        lastCP = spawn;
        Relevant.text = "";

        if (SceneManager.GetActiveScene().buildIndex == 0) {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = musica[0];
        }
        else if (SceneManager.GetActiveScene().buildIndex == bosslevels[1])
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = musica[4];//dragon
        }
        else {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = musica[1];
            }

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
        }

    public void setCP(Transform CP)
    {
        lastCP = CP;
    }

    private void Update()
    {
        if (player.GetComponent<CambioFormas>().currentHP == 0)
        {
            for(int i=0; i<bosslevels.Length; ++i)
            {
                if (bosslevels[i] == SceneManager.GetActiveScene().buildIndex)
                {
                    localBool = true;
                    break;
                } 
            }
            player.SetActive(false);
            Relevant.text = "Has muerto";
            counter += Time.deltaTime;
            Relevant.color = new Color((counter / 3) * 184, 0, 0, counter / 3);
                if (counter >= 6 )
                {
                    Relevant.text = "";
                    Relevant.color = new Color(0, 0, 0, 0);
                if (!localBool)
                {
                    player.SetActive(true);
                    player.GetComponent<Transform>().position = lastCP.position;

                    if (SceneManager.GetActiveScene().buildIndex == bosslevels[1])
                    {
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = musica[4];//dragon
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().clip = musica[1];
                    }

                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();


                if (boss != null)
                    {
                        Destroy(boss);
                        if (jefe == putoamo.mono)
                            hisSpawner.GetComponent<BoxCollider2D>().enabled = (true);
                    }


                    player.GetComponent<CambioFormas>().currentHP = 3;
                    player.GetComponent<CambioFormas>().invulnerable = false;
                    FindObjectOfType<ComportamientoCamara>().stopStick();
                    FindObjectOfType<ComportamientoCamara>().regularSize();
                    counter = 0;
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag=="humana" || collision.tag=="demonio") {
        
            player.GetComponent<CambioFormas>().killPlayer();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
