using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthOrbitBehavior : MonoBehaviour
{

    public Transform Earth;
    public Ellipse orbitPath;

    [Range(0f, 1f)]
    public float orbitProgress = 0f;
    public float orbitPeriod = 31557600f;
    public bool orbitActive = true;
    // Start is called before the first frame update
    void Start()
    {

        setEarthPosition();
        StartCoroutine(Animation());
    }
    void setEarthPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        Earth.localPosition = new Vector3(orbitPos.x,0f, orbitPos.y);
    }

    IEnumerator Animation()
    {
        float orbitSpeed = 1f / orbitPeriod;
        while(orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            setEarthPosition();
            yield return null;
        }
    }
}

