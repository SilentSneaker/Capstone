using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse
{
    public float xAxis;
    public float yAxis;

    public Ellipse(float xAxis, float yAxis)
    {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
    }

    public Vector2 Evaluate(float t)
    {
        float a = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(a) * xAxis;
        float y = Mathf.Cos(a) * yAxis;
        return new Vector2(x, y);
    }
}
