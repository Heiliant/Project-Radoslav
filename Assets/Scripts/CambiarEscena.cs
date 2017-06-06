using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour {
    public GameObject BotonContinuar;
    private int stage;

    private void Start()
    {
        stage = PlayerPrefs.GetInt("stage", 256);
        stage = 256;
        if (stage == 256)
        {
            BotonContinuar.GetComponent<Button>().interactable = false;
        }
    }
    public void cambiarEscenaTotal(int newScene) {
        SceneManager.LoadScene(newScene, LoadSceneMode.Single);
        PlayerPrefs.SetInt("jumps", 0);
        PlayerPrefs.SetInt("transf", 0);
        PlayerPrefs.SetInt("disparo", 0);
        PlayerPrefs.SetInt("stage", 0);
        PlayerPrefs.SetInt("bossMono", 1);
        PlayerPrefs.SetInt("bossSol", 1);
        PlayerPrefs.SetInt("bossDragon", 1);
        PlayerPrefs.SetFloat("sound", 0.1f);
    }

    public void cambiarEscenaAditivo(int newScene)
    {
        SceneManager.LoadScene(newScene, LoadSceneMode.Additive);
        
    }

    public void goOn(){
        int mylvl = PlayerPrefs.GetInt("stage", 256);
        SceneManager.LoadScene(mylvl, LoadSceneMode.Single);
    }

    public void endApplication()
    {
        Application.Quit();
    }
}
