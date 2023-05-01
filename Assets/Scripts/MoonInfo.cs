using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonInfo : MonoBehaviour
{
    public string[] moonInfo;
    public void Start()
    {
        // Load the text file
        TextAsset textAsset = Resources.Load<TextAsset>("MoonInfo");
        //Debug.Log("File path: " + Application.dataPath + "/Resources/" + filename + ".txt");


        // Split the text file into an array of lines
        string[] lines = textAsset.text.Split('\n');

        // Create a new array to store the data
        moonInfo = new string[lines.Length];

        // Copy the data into the new array
        for (int i = 0; i < lines.Length; i++)
        {
            moonInfo[i] = lines[i];
            //Debug.Log(lines[i]);
        }

    }


    public string GetInfo(int index)
    {
        return moonInfo[index];
    }

}
