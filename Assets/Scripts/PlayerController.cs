using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool modernTransition = false;
    public float transitionSpeed = 10.0f, transitionRotationSpeed = 500.0f;

    Vector3 targetPos, prePos, targetRotation;

    private void Start()
    {
        targetPos = Vector3Int.RoundToInt(transform.position);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    } 

    void MovePlayer()
    {
        if (true)
        {
            prePos = targetPos;

            Vector3 modifiedPosition = targetPos;

            if(targetRotation.y > 270.0f && targetRotation.y < 361.0f)
            {
                targetRotation.y = 0.0f;
            }

            if (targetRotation.y < 0.0f)
            {
                targetRotation.y = 270.0f;
            }

            if (!modernTransition)
            {
                transform.position = targetPos;
                transform.rotation = Quaternion.Euler(targetRotation);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * transitionSpeed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * transitionRotationSpeed);
            }
        }
        else
        {
            targetPos = prePos;
        }
    }

    #region Movement Functions

    public void RotateLeft()
    {
        if (canMove)
        {
            targetRotation -= Vector3.up * 90.0f;
        }
    }

    public void RotateRight()
    {
        if (canMove)
        {
            targetRotation += Vector3.up * 90.0f;
        }
    }

    public void MoveFoward()
    {
        if (canMove)
        {
            targetPos += transform.forward;
        }
    }

    public void MoveBackward()
    {
        if (canMove)
        {
            targetPos -= transform.forward;
        }
    }

    public void MoveRight()
    {
        if (canMove)
        {
            targetPos += transform.right;
        }
    }

    public void MoveLeft()
    {
        if (canMove)
        {
            targetPos -= transform.right;
        }
    }
    #endregion

    public bool canMove
    {
        get
        {
            if ((Vector3.Distance(transform.position, targetPos) < 0.05f) && (Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f))
            {
                //Debug.Log("can move");
                return true;
            }
            else
            {
                Debug.Log("current position is: " + transform.position + "\n target position is : " + targetPos + "\n current rotation is: " + transform.eulerAngles + "\n target rotation is: " + targetRotation);
                return false;
            }
        }
    }
}
