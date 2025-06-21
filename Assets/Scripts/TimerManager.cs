using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    public float timeLeft = 900f; // 15 min
    public TextMeshProUGUI timerText;

    private void Awake() {
        if (Instance == null) Instance = this;
    }

    void Update() {
        if (!GameManager.Instance.gameActive) return;

        timeLeft -= Time.deltaTime;
        UpdateUI();

        if (Mathf.Approximately(timeLeft, 600f)) EventTrigger.Instance.TriggerEvent(1); // minuto 5
        if (Mathf.Approximately(timeLeft, 300f)) EventTrigger.Instance.TriggerEvent(2); // minuto 10

        if (timeLeft <= 0f) GameManager.Instance.EndGame();
    }

    void UpdateUI() {
        int min = Mathf.FloorToInt(timeLeft / 60);
        int sec = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = $"{min:00}:{sec:00}";
    }

    public void StartTimer() {
        timeLeft = 900f;
    }
}

