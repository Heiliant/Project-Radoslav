using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlPauseMenu : MonoBehaviour {
    public bool control = false;
    private void FixedUpdate()
    {

    }

    public GameObject opcionesMenu;
    private void Start()
    {
        if(!control)
            gameObject.SetActive(false);
    }

    public void _Continue()
    {
        gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().stayQuiet(!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().inmovil);
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
    }

    public void _Quit()
    {
        Application.Quit();
    }
    public void _Options()
    {
        opcionesMenu.SetActive(true);
    }

    public void _Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void setVolume()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().volume = GetComponentInChildren<Slider>().value;
    }

    public void goTo (int a){
        SceneManager.LoadScene(a);
        }
}
