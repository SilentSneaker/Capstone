using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EarthOrbitBehavior : MonoBehaviour
{

    public Transform Earth;
    public Ellipse orbitPath;

    [Range(0f,1f)]
    public decimal orbitProgress = 0m;
    [Range(0f,1f)]
    public float orbitPosition;
    public float orbitPeriod;
    public bool orbitActive = true;
    float timeDelta = 0;


    public float rotationPeriod = 1f;

    // Start is called before the first frame update
    void Start()
    {

        setEarthPosition();
        StartCoroutine(Animation());
    }
    void setEarthPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate((float)orbitProgress);
        Earth.localPosition = new Vector3(orbitPos.x,0f, orbitPos.y);
        float rotationAngle;

        rotationAngle = Time.deltaTime * (360f/rotationPeriod);
        Earth.Rotate(0f, rotationAngle, 0f);
    }
    
    IEnumerator Animation()
    {
        decimal orbitSpeed = 1m / (decimal)orbitPeriod;
        while(orbitActive)
        { 
            timeDelta += Time.deltaTime;
            UnityEngine.Debug.Log(Earth.name + " " + DateController.DeltaDate.TotalSeconds.ToString()+ " orbitSpeed: " + orbitSpeed + " = " + (((decimal)DateController.DeltaDate.TotalSeconds) * orbitSpeed).ToString("F9"));
            orbitProgress = (((decimal)DateController.DeltaDate.TotalSeconds + (decimal)timeDelta) * orbitSpeed);
            UnityEngine.Debug.Log(Earth.name +" Orbit Progress: " + orbitProgress.ToString("F9"));
            orbitProgress %= 1m;
            orbitPosition = (float)orbitProgress;
            
            setEarthPosition();
            yield return null;
        }
    }
}


