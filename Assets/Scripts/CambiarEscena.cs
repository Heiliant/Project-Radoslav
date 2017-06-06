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
