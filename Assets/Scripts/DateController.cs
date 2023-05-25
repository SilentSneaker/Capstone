using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

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
        if(!string.IsNullOrEmpty(fController.SelectedDate.text))  
        {
            string dateString = fController.SelectedDate.text + " " + fController.Hours.text + ":" + fController.Minutes.text + ":" + fController.Seconds.text;
            dateString = dateString.Replace("\u200B", "");
            if(DateTime.TryParse(dateString.Trim(), out endDateTime))
            {
                DeltaDate = endDateTime - startDateTime;
                endYear = endDateTime.Year;
                endMonth = endDateTime.Month;
                endDay = endDateTime.Day;
                endHour = endDateTime.Hour;
                endMinute = endDateTime.Minute;
                endSecond = endDateTime.Second;
                UnityEngine.Debug.Log("Sun: "+endDateTime.ToString());
            }   
            else
                UnityEngine.Debug.Log("Sun Time TryParse Failed."+ dateString+"|");
        }


        else
            DeltaDate = DateTime.UtcNow - startDateTime;

        
    }
}
