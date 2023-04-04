using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    public KeyCode foward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode rotateLeft = KeyCode.Q;
    public KeyCode rotateRight = KeyCode.E;

    PlayerController control;

    private void Awake()
    {
        control = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(foward))
        {
            control.MoveFoward();
        }

        if (Input.GetKeyUp(backward))
        {
            control.MoveBackward();
        }

        if (Input.GetKeyUp(left))
        {
            control.MoveLeft();
        }

        if (Input.GetKeyUp(right))
        {
            control.MoveRight();
        }

        if (Input.GetKeyUp(rotateLeft))
        {
            control.RotateLeft();
        }

        if (Input.GetKeyUp(rotateRight))
        {
            control.RotateRight();
        }
    }
}
