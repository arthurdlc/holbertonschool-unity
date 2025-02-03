using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    private float elapsedTime = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100) % 100);
        TimerText.text = string.Format("{0}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
}