using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustUserInfo : MonoBehaviour
{
    float userWeight;
    public FirebaseController firebase;
    // Start is called before the first frame update
    void Start()
    {
        userWeight = firebase.GetWeight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalculateWeight(float gravity)
    {
        float newWeight = gravity * userWeight;
        return newWeight;
    }
}
