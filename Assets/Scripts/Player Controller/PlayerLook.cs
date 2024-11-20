using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    private float sensitivity;

   public void ProcessLook(Vector2 input)
    {
        sensitivity = PlayerPrefs.GetInt("sensitivity", 20);
        var mouseX = input.x;
        var mouseY = input.y;
        //calculate rotation
        xRotation -= (mouseY * Time.deltaTime) * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //rotate left/right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * sensitivity);

    }
}
