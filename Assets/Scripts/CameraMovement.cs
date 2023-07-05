using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Controls the speed of camera movement
    public float panSpeed = 250f;
    public float rotateSpeed = 500f;
    public float zoomSpeed = 250f;
    public float swipeSpeed = 250f;
    public float rotationSpeed = 1f;
    private float initialDistance = 0f;
    private Vector2[] touchPositions = new Vector2[2];
    private Vector2 touchStartPosition;

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

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 swipeDelta = touch.position - touchStartPosition;

                float swipeMagnitudeX = Mathf.Abs(swipeDelta.x);
                float swipeMagnitudeY = Mathf.Abs(swipeDelta.y);
                float swipeDirectionX = Mathf.Sign(swipeDelta.x);
                float swipeDirectionY = Mathf.Sign(swipeDelta.y);

                // Adjust the camera's position based on swipe direction and magnitude
                transform.position += new Vector3(swipeDirectionX * swipeMagnitudeX * swipeSpeed * Time.deltaTime,
                                                   swipeDirectionY * swipeMagnitudeY * swipeSpeed * Time.deltaTime,
                                                   0f);
            }
        }

        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                // Store the initial positions of both touches
                touchPositions[0] = touch1.position;
                touchPositions[1] = touch2.position;

            }
            else if(touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                // Calculate the delta positions of both touches
                Vector2 touch1Delta = touch1.position - touchPositions[0];
                Vector2 touch2Delta = touch2.position - touchPositions[1];

                // Calculate the rotation angle based on the average delta position
                float rotationAngle = (touch1Delta.x + touch2Delta.x) * 0.5f * rotationSpeed * Time.deltaTime;

                // Rotate the camera around its up axis by the calculated angle
                transform.Rotate(Vector3.up, rotationAngle, Space.World);

                // Update the touch positions for the next frame
                touchPositions[0] = touch1.position;
                touchPositions[1] = touch2.position;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                // Calculate the current distance between the two touches
                float currentDistance = Vector2.Distance(touch1.position, touch2.position);

                // Calculate the difference in distance from the initial distance
                float zoomAmount = (currentDistance - initialDistance) * zoomSpeed * Time.deltaTime;

                // Move the camera along its forward axis by the calculated amount
                transform.Translate(Vector3.forward * zoomAmount, Space.Self);

                // Update the initial distance for the next frame
                initialDistance = currentDistance;
            }
        }
    }
}
