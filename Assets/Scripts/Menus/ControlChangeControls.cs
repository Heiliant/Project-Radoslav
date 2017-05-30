using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlChangeControls : MonoBehaviour {

	public void _Back()
    {
        gameObject.SetActive(false);
    }

    public void _ChangeKey()
    {
        StartCoroutine(changeInput());
    }

    IEnumerator changeInput()
    {
        yield return new WaitUntil(Input.)
    }
}
