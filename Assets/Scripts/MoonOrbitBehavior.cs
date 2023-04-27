using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonOrbitBehavior : MonoBehaviour
{

    public Transform Moon;
    public Ellipse orbitPath;

    [Range(0f, 1f)]
    public float orbitProgress = 0f;
    public float orbitPeriod = 2358720f;
    public bool orbitActive = true;
    // Start is called before the first frame update
    void Start()
    {

        setMoonPosition();
        StartCoroutine(Animation());
    }
    void setMoonPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        Moon.localPosition = new Vector3(orbitPos.x,0f, orbitPos.y);
    }

    IEnumerator Animation()
    {
        float orbitSpeed = 1f / orbitPeriod;
        while(orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            setMoonPosition();
            yield return null;
        }
    }
}

