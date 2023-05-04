using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Controls the speed of camera movement
    public float panSpeed = 1f;
    public float rotateSpeed = 1000f;
    public float zoomSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get current axis movement of the mouse
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        // Rotate camera if right mouse button is held down
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(0, horizontal * rotateSpeed * Time.deltaTime, 0, Space.World);
            transform.Rotate(-vertical * rotateSpeed * Time.deltaTime, 0, 0, Space.Self);
        }

        // Move camera along the X and Y axes if left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            transform.Translate(-Input.GetAxis("Mouse X") * panSpeed, -Input.GetAxis("Mouse Y") * panSpeed, 0, Space.Self);
        }

        // Zoom camera in and out if the mouse wheel is spun
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
        }

    }
}
