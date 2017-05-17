using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAura : MonoBehaviour {
    public float vel;
    public float scaleSpeed;
    public float maxSize;
    public float minSize;
    public bool decreaseScale;
    private Vector2 initialScale;
    public bool resetAuraScale;

    private float counter = 0f;
	// Use this for initialization

	void Start () {
        initialScale = GetComponent<Transform>().localScale;
        resetAuraScale = false;
	}

    private void resetScale()
    {
        if (GetComponent<Transform>().localScale.x != initialScale.x || GetComponent<Transform>().localScale.y != initialScale.y)
        {
            if (GetComponent<Transform>().localScale.x < initialScale.x && GetComponent<Transform>().localScale.y < initialScale.y)
            {
                GetComponent<Transform>().localScale = new Vector2(GetComponent<Transform>().localScale.x + (Mathf.Abs(GetComponent<Transform>().localScale.x - initialScale.x)) / 10,
                    GetComponent<Transform>().localScale.y + (Mathf.Abs(GetComponent<Transform>().localScale.y - initialScale.y) / 10));
            }
            else if (GetComponent<Transform>().localScale.x > initialScale.x || GetComponent<Transform>().localScale.y > initialScale.y)
            {
                GetComponent<Transform>().localScale = new Vector2(GetComponent<Transform>().localScale.x - (Mathf.Abs(GetComponent<Transform>().localScale.x - initialScale.x)) / 10,
                    GetComponent<Transform>().localScale.y - (Mathf.Abs(GetComponent<Transform>().localScale.y - initialScale.y) / 10));
            }
        }
        if ((GetComponent<Transform>().localScale.x <= initialScale.x * 1.08 && GetComponent<Transform>().localScale.x >= initialScale.x * 0.92f) &&
            (GetComponent<Transform>().localScale.y <= initialScale.y * 1.08 && GetComponent<Transform>().localScale.y >= initialScale.y * 0.92f))
            resetAuraScale = false;
    }

    private void FixedUpdate()
    {
        if (!resetAuraScale)
        {
            if (GetComponent<Transform>().localScale.x < minSize && GetComponent<Transform>().localScale.y < minSize)
            {
                decreaseScale = false;
            }
            else if (GetComponent<Transform>().localScale.x > maxSize && GetComponent<Transform>().localScale.y > maxSize)
            {
                decreaseScale = true;
            }
        }

    }

    // Update is called once per frame
    void Update () {
        GetComponent<Transform>().Rotate(0, 0, Time.deltaTime*vel);
        if (!resetAuraScale)
        {
            if (!decreaseScale)
                GetComponent<Transform>().localScale = new Vector3(GetComponent<Transform>().localScale.x + Time.deltaTime * scaleSpeed,
                    GetComponent<Transform>().localScale.y + Time.deltaTime * scaleSpeed, 1);
            else
                GetComponent<Transform>().localScale = new Vector3(GetComponent<Transform>().localScale.x - Time.deltaTime * scaleSpeed,
                    GetComponent<Transform>().localScale.y - Time.deltaTime * scaleSpeed, 1);
        }
        else
            resetScale();
    }

}
