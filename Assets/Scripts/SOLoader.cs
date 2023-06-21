using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOLoader : MonoBehaviour
{
    public SpaceObject spaceObject;
    public float gravity;

    // Start is called before the first frame update
    void Start()
    {
        gravity = spaceObject.gravity;
    }

}
