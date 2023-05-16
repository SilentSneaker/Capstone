using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DateController : MonoBehaviour
{
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
        
        endDateTime = new DateTime(endYear,endMonth,endDay,endHour,endMinute,endSecond);
            if(dateChanged)
                DeltaDate = endDateTime - startDateTime;
            else
                DeltaDate = DateTime.UtcNow - startDateTime;
    }
}
