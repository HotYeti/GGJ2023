using System.Collections;
using Helpers;
using TMPro;
using UnityEngine;

public class Timer : Singleton<Timer>
{
    private bool timer1Running = false;
    private bool timer2Running = false;
    private float timer1ElapsedTime = 0f;
    private float timer2ElapsedTime = 0f;
    [SerializeField] private TextMeshProUGUI TimerObject;

    private void Start()
    {
        StartCoroutine(StartTimers());
    }

    public void SetTimerPlayer(int playerID)
    {
        if (playerID == 1)
        {
            Debug.Log("Player 1 Timer Started!");
            timer1Running = false;
        } else if (playerID == 2)
        {
            Debug.Log("Player 2 Timer Started!");
            timer2Running = false;
        }
    }
    
    private IEnumerator StartTimers()
    {
        float timer1Duration = 180f;
        float timer2Duration = 180f;

        while (true)
        {
            if (!timer1Running)
            {
                timer1Running = true;
                float timer1StartTime = Time.time;

                while (Time.time - timer1StartTime < timer1Duration - timer1ElapsedTime)
                {
                    TimerObject.text = (timer2Duration - timer2ElapsedTime).ToString();
                    yield return null;

                    if (timer2Running)
                    {
                        timer1ElapsedTime += Time.time - timer1StartTime;
                        break;
                    }
                }

                timer1Running = false;
                timer1ElapsedTime = 0f;
            }
            else if (!timer2Running)
            {
                timer2Running = true;
                float timer2StartTime = Time.time;

                while (Time.time - timer2StartTime < timer2Duration - timer2ElapsedTime)
                {
                    TimerObject.text = (timer2Duration - timer2ElapsedTime).ToString();
                    yield return null;

                    if (timer1Running)
                    {
                        timer2ElapsedTime += Time.time - timer2StartTime;
                        break;
                    }
                }

                timer2Running = false;
                timer2ElapsedTime = 0f;
            }
        }
    }
}
