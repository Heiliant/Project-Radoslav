using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour {
	public void cambiarEscenaTotal (int newScene) {
        SceneManager.LoadScene(newScene, LoadSceneMode.Single);
	}

    public void cambiarEscenaAditivo(int newScene)
    {
        SceneManager.LoadScene(newScene, LoadSceneMode.Additive);
    }

    public void endApplication()
    {
        Application.Quit();
    }
}
