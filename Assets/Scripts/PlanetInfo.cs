using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    public string[] planetInfo;

    public void Start()
    {
        // Load the text file
        TextAsset textAsset = Resources.Load<TextAsset>("PlanetInfo");
        //Debug.Log("File path: " + Application.dataPath + "/Resources/" + filename + ".txt");


        // Split the text file into an array of lines
        string[] lines = textAsset.text.Split('\n');

        // Create a new array to store the data
        planetInfo = new string[lines.Length];

        // Copy the data into the new array
        for (int i = 0; i < lines.Length; i++)
        {
            planetInfo[i] = lines[i];
            //Debug.Log(lines[i]);
        }

    }

    public string GetInfo(int index)
    {
        return planetInfo[index];
    }
}
