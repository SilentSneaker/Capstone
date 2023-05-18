using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DateController : MonoBehaviour
{



    public FirebaseController fController;
    
    
    public static DateTime startDateTime = new DateTime(1982, 3, 10, 0, 0, 0);
    public static DateTime Now = DateTime.Now;
    public static TimeSpan DeltaDate = DateTime.UtcNow - startDateTime;
    public int endYear = 2023;
    public int endMonth = 1;
    public int endDay = 1;
    public int endHour;
    public int endMinute;
    public int endSecond;
    public DateTime endDateTime;
    public bool dateChanged = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(string.IsNullOrEmpty(fController.Hours.text))
            fController.Hours.text = "00";
        if(string.IsNullOrEmpty(fController.Minutes.text))
            fController.Minutes.text = "00";
        if(string.IsNullOrEmpty(fController.Seconds.text))
            fController.Seconds.text = "00";
        if(DateTime.TryParse(fController.SelectedDate + " " + fController.Hours + ":" + fController.Minutes + ":" + fController.Seconds, out endDateTime))
            {
                
            }
        if(!string.IsNullOrEmpty(fController.SelectedDate.text))
            DeltaDate = endDateTime - startDateTime;
        else
            DeltaDate = DateTime.UtcNow - startDateTime;

        UnityEngine.Debug.Log(endDateTime.ToString());
    }
}
