using System.Collections;
using Helpers;
using UnityEngine;
using TMPro;

public class Timer : Singleton<Timer>
{
    public TextMeshProUGUI timerText;
    [SerializeField]
    private float timerDuration = 300f;
    private float timer1ElapsedTime = 0f;
    private float timer2ElapsedTime = 0f;
    private Coroutine timer1Coroutine;
    private Coroutine timer2Coroutine;
    
    public void SetTimerPlayer(int playerID)
    {
        if (playerID == 1)
        {
            //Debug.Log("Player 1 Timer Started!");
            PauseTimer2();
            StartTimer1();
        } else if (playerID == 2)
        {
            //Debug.Log("Player 2 Timer Started!");
            PauseTimer1();
            StartTimer2();
        }
    }

    private IEnumerator Timer1Coroutine()
    {
        while (timer1ElapsedTime < timerDuration)
        {
            float timeRemaining = timerDuration - timer1ElapsedTime;
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return null;
            timer1ElapsedTime += Time.deltaTime;
        }

        timer1ElapsedTime = 0f;
        timerText.text = "00:00";

        StartCoroutine(StoryManager.Instance.TimesUpPopup());
        GameManager.Instance.EndGame(2);
    }

    private IEnumerator Timer2Coroutine()
    {
        while (timer2ElapsedTime < timerDuration)
        {
            float timeRemaining = timerDuration - timer2ElapsedTime;
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return null;
            timer2ElapsedTime += Time.deltaTime;
        }

        timer2ElapsedTime = 0f;
        timerText.text = "00:00";
        
        StartCoroutine(StoryManager.Instance.TimesUpPopup());
        GameManager.Instance.EndGame(1);
    }

    public void StartTimer1()
    {
        timer1Coroutine = StartCoroutine(Timer1Coroutine());
    }

    public void StartTimer2()
    {
        timer2Coroutine = StartCoroutine(Timer2Coroutine());
    }

    public void PauseTimer1()
    {
        if (timer1Coroutine == null) return;
        StopCoroutine(timer1Coroutine);
    }

    public void PauseTimer2()
    {
        if (timer2Coroutine == null) return;
        StopCoroutine(timer2Coroutine);
    }
}
