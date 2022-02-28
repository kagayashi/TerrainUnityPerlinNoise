using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private float MouseX;
    private float MouseY;
    float xRotation = 0f;

    public float sensetivity = 200f;

    public Transform Player;
    public GameObject PauseMenu;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }
    private void Update()
    {
        if (PauseMenu.activeInHierarchy == false)
        {
            MouseX = Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;
            MouseY = Input.GetAxis("Mouse Y") * sensetivity * Time.deltaTime;

            xRotation -= MouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation ,0, 0);
            Player.Rotate(Vector3.up * MouseX);
        }
    }
}
