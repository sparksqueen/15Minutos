using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject mainUI;
    public GameObject gameOverScreen;
    public GameObject titleScreen;
    public GameObject caosometroUI;
    public Image gameOverBackground;
    public Sprite finalPerfecto;
    public Sprite finalDesordenado;
    public Sprite finalCatastrofico;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Ocultar caosómetro al inicio
        if (caosometroUI != null)
            caosometroUI.SetActive(false);

        // Ocultar pantalla de Game Over
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);

        // // Ocultar animación de introducción si está asignada
        // if (introAnimacionGO != null)
        //     introAnimacionGO.SetActive(false);
    }

    public void OnPlayPressed()
    {
        if (titleScreen != null) titleScreen.SetActive(false);
        if (mainUI != null) mainUI.SetActive(true);
        if (caosometroUI != null) caosometroUI.SetActive(true);

        // // 🔒 Desactivar movimiento del jugador
        // var jugador = GameObject.FindGameObjectWithTag("Player");
        // if (jugador != null)
        // {
        //     var movimiento = jugador.GetComponent<Movement>();
        //     if (movimiento != null)
        //         movimiento.SetMovimientoHabilitado(false);
        // }

        // // Activar intro al empezar el juego
        // if (introAnimacionGO != null)
        //     introAnimacionGO.SetActive(true);

        GameManager.Instance.StartGame();
    }

    public void ShowGameOver(string finalText)
    {
        Debug.Log("FINAL: " + finalText);

        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);

        var texto = gameOverScreen.GetComponentInChildren<TextMeshProUGUI>();
        if (texto != null)
            texto.text = finalText;

        if (gameOverBackground != null)
        {
            if (finalText.ToLower().Contains("perfecto"))
            {
                gameOverBackground.sprite = finalPerfecto;
                Debug.Log("SPRITE ASIGNADO: " + finalPerfecto.name);
            }
            else if (finalText.ToLower().Contains("desordenado"))
            {
                gameOverBackground.sprite = finalDesordenado;
                Debug.Log("SPRITE ASIGNADO: " + finalDesordenado.name);
            }
            else
            {
                gameOverBackground.sprite = finalCatastrofico;
                Debug.Log("SPRITE ASIGNADO: " + finalCatastrofico.name);
            }

            gameOverBackground.enabled = false;
            gameOverBackground.enabled = true; // 🔁 Forzar refresco visual
        }

        if (caosometroUI != null)
            caosometroUI.SetActive(false);
    }
}
