using System.Collections;
using Helpers;
using UnityEngine;
using TMPro;

public class Timer : Singleton<Timer>
{
    public TextMeshProUGUI timerText;
    private float timer1Duration = 180f;
    private float timer2Duration = 180f;
    private float timer1ElapsedTime = 0f;
    private float timer2ElapsedTime = 0f;
    private bool timer1Running = false;
    private bool timer2Running = false;
    private Coroutine timer1Coroutine;
    private Coroutine timer2Coroutine;
    
    public void SetTimerPlayer(int playerID)
    {
        if (playerID == 1)
        {
            Debug.Log("Player 1 Timer Started!");
            PauseTimer2();
            StartTimer1();
        } else if (playerID == 2)
        {
            Debug.Log("Player 2 Timer Started!");
            PauseTimer1();
            StartTimer2();
        }
    }

    private IEnumerator Timer1Coroutine()
    {
        while (timer1ElapsedTime < timer1Duration)
        {
            timer1ElapsedTime += Time.deltaTime;
            float timeRemaining = timer1Duration - timer1ElapsedTime;
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return null;
        }

        timer1Running = false;
        timer1ElapsedTime = 0f;
        timerText.text = "00:00";
    }

    private IEnumerator Timer2Coroutine()
    {
        while (timer2ElapsedTime < timer2Duration)
        {
            timer2ElapsedTime += Time.deltaTime;
            float timeRemaining = timer2Duration - timer2ElapsedTime;
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return null;
        }

        timer2Running = false;
        timer2ElapsedTime = 0f;
        timerText.text = "00:00";
    }

    public void StartTimer1()
    {
        timer1Running = true;
        timer2Running = false;
        timer1Coroutine = StartCoroutine(Timer1Coroutine());
    }

    public void StartTimer2()
    {
        timer1Running = false;
        timer2Running = true;
        timer2Coroutine = StartCoroutine(Timer2Coroutine());
    }

    public void PauseTimer1()
    {
        if (timer1Coroutine == null) return;
        timer1Running = false;
        StopCoroutine(timer1Coroutine);
    }

    public void PauseTimer2()
    {
        if (timer2Coroutine == null) return;
        timer2Running = false;
        StopCoroutine(timer2Coroutine);
    }
}
