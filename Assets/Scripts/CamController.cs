using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float Sensetivity = 20;
    float rotationX;
    float rotationY;

    void Update ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rotationX += -Input.GetAxis("Mouse Y") * Sensetivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse X") * Sensetivity * Time.deltaTime;
        transform.parent.localEulerAngles = new Vector3(rotationX, rotationY, 0);
	}
}
