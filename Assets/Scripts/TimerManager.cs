using System.Collections;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    public TextMeshProUGUI timerText;

    public float duracionReal = 480f; // 8 minutos reales
    public float duracionVisible = 900f; // 15 minutos visibles en el reloj

    private float tiempoTranscurrido = 0f;
    private bool juegoTerminado = false;
    private bool evento1 = false;
    private bool evento2 = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.gameActive || juegoTerminado) return;

        tiempoTranscurrido += Time.deltaTime;

        float proporcion = Mathf.Clamp01(tiempoTranscurrido / duracionReal);
        float tiempoVisibleRestante = duracionVisible * (1 - proporcion);

        if (!evento1 && tiempoVisibleRestante <= 600f)
        {
            EventTrigger.Instance.TriggerEvent(1);
            evento1 = true;
        }

        if (!evento2 && tiempoVisibleRestante <= 300f)
        {
            EventTrigger.Instance.TriggerEvent(2);
            evento2 = true;
        }

        if (tiempoTranscurrido >= duracionReal)
        {
            GameManager.Instance.EndGame();
            juegoTerminado = true;
            tiempoVisibleRestante = 0;
        }

        UpdateUI(tiempoVisibleRestante);
    }

    void UpdateUI(float visibleTime)
    {
        int min = Mathf.FloorToInt(visibleTime / 60);
        int sec = Mathf.FloorToInt(visibleTime % 60);
        timerText.text = $"{min:00}:{sec:00}";
    }

    public void StartTimer()
    {
        tiempoTranscurrido = 0f;
        juegoTerminado = false;
        evento1 = false;
        evento2 = false;
    }
}
