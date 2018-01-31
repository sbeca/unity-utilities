using UnityEngine;
using UnityEngine.UI;
using System;

public class AllocationFreeTimerText : MonoBehaviour
{
    public Text MinutesText;
    public Text SecondsText;
    public float UpdateRate = 1f;

    private const int minutesCacheCount = 100;
    private static string[] minutesCache = null;
    private const int secondsCacheCount = 60;
    private static string[] secondsCache = null;

    private bool hasStarted = false;
    private float timerStartTime;
    private float lastUpdateTime;
    private bool isCountingDown = false;
    private float countDownTimeInSeconds = -1;
    private Action countdownCallback = null;

    private void Awake()
    {
        if (minutesCache == null) {
            minutesCache = new string[minutesCacheCount];
            for (int i = 0; i < minutesCacheCount; i++) {
                minutesCache[i] = i.ToString();
            }
        }

        if (secondsCache == null) {
            secondsCache = new string[secondsCacheCount];
            for (int i = 0; i < secondsCacheCount; i++) {
                secondsCache[i] = i.ToString(":00");
            }
        }
    }

    private void Update()
    {
        if (!hasStarted) return;

        if ((Time.time - lastUpdateTime) >= UpdateRate) {
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        if (!hasStarted) return;

        float currentTime = 0f;

        if (isCountingDown) {
            currentTime = countDownTimeInSeconds - (Time.time - timerStartTime);
            if (currentTime <= 0f) {
                if (countdownCallback != null) countdownCallback();
                hasStarted = false;
            }
        } else {
            currentTime = Time.time - timerStartTime;
        }

        if (currentTime < 0f) currentTime = 0f;
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        MinutesText.text = (minutes >= minutesCacheCount ? minutes.ToString() : minutesCache[minutes]);
        SecondsText.text = (seconds >= secondsCacheCount ? seconds.ToString() : secondsCache[seconds]);

        lastUpdateTime = Time.time;
    }

    public void StartCountDown(float countDownTimeInSeconds)
    {
        timerStartTime = Time.time;
        this.countDownTimeInSeconds = countDownTimeInSeconds;
        isCountingDown = true;
        hasStarted = true;
        UpdateDisplay();
    }

    public void StartCountUp()
    {
        timerStartTime = Time.time;
        isCountingDown = false;
        hasStarted = true;
        this.countDownTimeInSeconds = -1f;
        UpdateDisplay();
    }

    public void StopCount(bool doFinalUpdate = true)
    {
        if (doFinalUpdate) UpdateDisplay();
        hasStarted = false;
    }
}
