using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltAsteroid : MonoBehaviour
{
    [SerializeField]
    private float orbitSpeed;
    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private bool rotationClockwise;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Vector3 rotationDirection;
    [SerializeField]
    private float activationDistance = 40000f;
    [SerializeField]
    private float distance;

    private void Start() {

    }

    public void SetupAsteriod(float _speed, float _rotation, GameObject _parent, bool _rotationClockwise)
    {
        orbitSpeed = _speed;
        rotationSpeed = _rotation;
        parent = _parent;
        rotationClockwise = _rotationClockwise;
        rotationDirection = new Vector3(Random.Range(0, 360),Random.Range(0, 360),Random.Range(0, 360));
    }
    // Update is called once per frame
     void Update()
     {
        Renderer ren = GetComponent<Renderer>();
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if (distance<=activationDistance)
        {
            ren.enabled = true;

        }
        else
        {
            ren.enabled = false;

        }
        if(rotationClockwise)
        {
            transform.RotateAround(parent.transform.position, parent.transform.up, orbitSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(parent.transform.position, -parent.transform.up, orbitSpeed * Time.deltaTime);
        }
            transform.Rotate(rotationDirection,rotationSpeed*Time.deltaTime);
        }
}
