﻿using UnityEngine;
using System.Collections;

public class SlowMotion : MonoBehaviour
{

    float currentAmount = 0f;
    float maxAmount = 5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("z"))
        {

            if (Time.timeScale == 1.0f)
                Time.timeScale = 0.2f;

            else

                Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.01f * Time.timeScale;
        }


        if (Time.timeScale == 0.02f)
        {

            currentAmount += Time.deltaTime;
        }

        if (currentAmount > maxAmount)
        {

            currentAmount = 0f;
            Time.timeScale = 1.0f;

        }

    }
}
