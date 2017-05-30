using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuKeysText : MonoBehaviour {
    public enum key{
        puño, salto, izquierda, derecha, esc, disparo, transform
    };

    public key PJInput;

    private void FixedUpdate()
    {
        switch (PJInput) {
            case key.derecha: GetComponentInChildren<Text>().text = FindObjectOfType<PlayerControl>().getRight().ToString();
                break;
            case key.izquierda:
                GetComponentInChildren<Text>().text = FindObjectOfType<PlayerControl>().getLeft().ToString();
                break;
            case key.salto:
                GetComponentInChildren<Text>().text = FindObjectOfType<PlayerControl>().getJump().ToString();
                break;
            case key.esc:
                GetComponentInChildren<Text>().text = FindObjectOfType<CambioFormas>().getEsc().ToString();
                break;
            case key.puño:
                GetComponentInChildren<Text>().text = FindObjectOfType<PlayerControl>().getFist().ToString();
                break;
            case key.disparo:
                break;
            case key.transform:
                GetComponentInChildren<Text>().text = FindObjectOfType<CambioFormas>().getTransf().ToString();
                break;
        }
    }
}
