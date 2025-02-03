using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockCounter : MonoBehaviour
{
    public TextMeshProUGUI timer;
    float minutes = 0;
    float seconds = 0;
    float miliseconds = 0;

    void Update()
    {
        if (miliseconds >= 99)
        {
            if (seconds >= 59)
            {
                minutes++;
                seconds = 0;
            }
            else if (seconds >= 0)
            {
                seconds++;
            }

            miliseconds = 0;
        }

        miliseconds += Time.deltaTime * 100;
        
        //display the formatted result
        timer.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, (int)miliseconds);
    }
}
