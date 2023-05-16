using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class EarthPython : MonoBehaviour
{
    ProcessStartInfo start = new ProcessStartInfo();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        start.FileName = "PlanetPosition.py";  // Set the name of the Python executable
        start.Arguments = "hello.py arg1 arg2";  // Set the arguments to pass to the Python script
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;

// Start the process
        Process process = new Process();
        process.StartInfo = start;
        process.Start();
    }
}
