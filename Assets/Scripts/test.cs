using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [Header("Movement Settings")]
    public bool modernWalkTransition = false;
    public float transitionSpeed = 10.0f, transitionRotationSpeed = 500.0f;

    [Header("Inputs")]
    public KeyCode foward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode rotateLeft = KeyCode.Q;
    public KeyCode rotateRight = KeyCode.E;

    Vector3 targetPosition, currentPosition, previousPosition, targetRotation, rotatedFoward;

    public bool canMove;
    private int rotationCount;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = Vector3Int.RoundToInt(transform.position);
        //targetRotation = transform.eulerAngles;
        //previousPosition = transform.position;
    }
    
    private void FixedUpdate()
    {
        Movement();
        RotateCharacter();
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position + "\n" + targetPosition);
    }

    private void LateUpdate()
    {
        MovementCheck();
        //FowardFaceNRotation();
    }

    public void MovementCheck()
    {
        Vector3 transX = new Vector3(transform.position.x, 0, 0);
        Vector3 transZ = new Vector3(0, 0, transform.position.z);

        if ((Vector3.Distance(transX, targetPosition) < 0.05f) && (Vector3.Distance(transZ, targetPosition) < .005f) && (Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f))
        {
            canMove = true;
        }
        else
        {
            canMove = false;
            Debug.Log("current position is: " + transform.position + "\n target position is : " + targetPosition + "\n current rotation is: " + transform.eulerAngles + "\n target rotation is: " + targetRotation);
        }
    }

    #region Character Rotation
    void RotateCharacter()
    {
        if (canMove)
        {
            if (Input.GetKeyUp(rotateLeft))
            {

                targetRotation -= Vector3.up * 90.0f;
                if (rotationCount == 0)
                {
                    rotationCount = 3;
                }
                else
                {
                    rotationCount--;
                }

            }

            if (Input.GetKeyUp(rotateRight))
            {
                targetRotation += Vector3.up * 90.0f;
                rotationCount++;
            }
        }


    }

    //void FowardFaceNRotation()
    //{
    //    if(rotationCount == 0)
    //    {
    //        rotatedFoward = targetPosition += transform.forward;
    //    }

    //    if (rotationCount == 1)
    //    {
    //        rotatedFoward = targetPosition += Vector3.right;
    //    }

    //    if (rotationCount == 2)
    //    {
    //        rotatedFoward = targetPosition -= Vector3.forward;
    //    }

    //    if (rotationCount == 3)
    //    {
    //        rotatedFoward = targetPosition -= Vector3.right;
    //    }
    //}
    #endregion

    void Movement()
    {
        if (canMove)
        {
            previousPosition = targetPosition;
            //incase player position modification is needed
            Vector3 temp = targetPosition;

            //wraps around and checks the rotation of the charcter setting its rotation back to 0 if it rotates past one of these degrees
            if (targetRotation.y > 270.0f && targetRotation.y < 361)
            {
                targetRotation.y = 0.0f;
            }
            if (targetRotation.y < 0.0f)
            {
                targetRotation.y = 270.0f;
            }


            if (!modernWalkTransition)
            {
                if (Input.GetKeyUp(foward))
                {
                    targetPosition += transform.forward;

                    Debug.Log("Moving Foward");
                    transform.position = targetPosition;
                    transform.rotation = Quaternion.Euler(targetRotation);
                    //try without returns
                }

                if (Input.GetKeyUp(backward))
                {
                    targetPosition -= transform.forward;

                    Debug.Log("Moving Backward");
                    transform.position = targetPosition;
                    transform.rotation = Quaternion.Euler(targetRotation);
                    //return;
                }

                if (Input.GetKeyUp(right))
                {
                    targetPosition += transform.right;

                    Debug.Log("Moving Right");
                    transform.position = targetPosition;
                    transform.rotation = Quaternion.Euler(targetRotation);
                    //return;
                }

                if (Input.GetKeyUp(left))
                {
                    targetPosition -= transform.right;

                    Debug.Log("Moving Left");
                    transform.position = targetPosition;
                    transform.rotation = Quaternion.Euler(targetRotation);
                    //return;
                }


            }

        }
        else
        {
            targetPosition = previousPosition;
        }

    }
}
