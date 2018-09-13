using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTransparency : MonoBehaviour {

    private float duration = 1.0f;
    Color textureColor;
    Material mat;

    void Start()
    {
        textureColor = GetComponent<Renderer>().material.color;
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        textureColor.a = Mathf.Min(Time.time, duration) / duration;
        mat.color = textureColor;

    }
}
