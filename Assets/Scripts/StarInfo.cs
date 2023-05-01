using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarInfo : MonoBehaviour
{
    public string[] starInfo;

    public void Start()
    {
        // Load the text file
        TextAsset textAsset = Resources.Load<TextAsset>("SunInfo");
        //Debug.Log("File path: " + Application.dataPath + "/Resources/" + filename + ".txt");


        // Split the text file into an array of lines
        string[] lines = textAsset.text.Split('\n');

        // Create a new array to store the data
        starInfo = new string[lines.Length];

        // Copy the data into the new array
        for (int i = 0; i < lines.Length; i++)
        {
            starInfo[i] = lines[i];
            //Debug.Log(lines[i]);
        }

    }

    public string GetInfo(int index)
    {
        return starInfo[index];
    }
}
