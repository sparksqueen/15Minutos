using System.Collections;
using System.Collections.Generic;
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

        // También por seguridad, ocultamos pantalla de GameOver al iniciar
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);
    }

    public void OnPlayPressed()
    {
        if (titleScreen != null) titleScreen.SetActive(false);
        if (mainUI != null) mainUI.SetActive(true);
        if (caosometroUI != null) caosometroUI.SetActive(true);

        GameManager.Instance.StartGame();
    }

    public void ShowGameOver(string finalText)
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);

            var texto = gameOverScreen.GetComponentInChildren<TextMeshProUGUI>();
            if (texto != null)
                texto.text = finalText;

            if (finalText.ToLower().Contains("perfecto"))
                gameOverBackground.sprite = finalPerfecto;
            else if (finalText.ToLower().Contains("desordenado"))
                gameOverBackground.sprite = finalDesordenado;
            else
                gameOverBackground.sprite = finalCatastrofico;
        }

        if (caosometroUI != null)
            caosometroUI.SetActive(false);
    }
}
