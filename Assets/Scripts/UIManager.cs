using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject mainUI;
    public GameObject gameOverScreen;
    public GameObject titleScreen;
    public GameObject caosometroUI;
    // public Sprite forzarSprite1;
    public GameObject introAnimacionGO; // Objeto con SpriteRenderer y Animator
    // public Image gameOverBackground;
    // public Sprite finalPerfecto;
    // public Sprite finalDesordenado;
    // public Sprite finalCatastrofico;
    public GameObject finalPerfectoUI; // Asignalo desde el inspector

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

        // Ocultar animación de introducción si está asignada
        if (introAnimacionGO != null)
            introAnimacionGO.SetActive(false);
    }

    public void OnPlayPressed()
    {
        if (titleScreen != null) titleScreen.SetActive(false);
        if (mainUI != null) mainUI.SetActive(true);
        if (caosometroUI != null) caosometroUI.SetActive(true);

        // 🔒 Desactivar movimiento del jugador
        var jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            var movimiento = jugador.GetComponent<Movement>();
            if (movimiento != null)
                movimiento.SetMovimientoHabilitado(false);
        }

        // Activar intro al empezar el juego
        if (introAnimacionGO != null)
            introAnimacionGO.SetActive(true);

        GameManager.Instance.StartGame();
    }

public void ShowGameOver(string finalText)
{
    if (gameOverScreen != null)
    {
        gameOverScreen.SetActive(true);
    }
    Time.timeScale = 0f; // dejalo por ahora
}



    public void ShowFinalPerfecto()
    {
        if (caosometroUI != null)
            caosometroUI.SetActive(false);


        if (finalPerfectoUI != null)
            finalPerfectoUI.SetActive(true);
    }

}
