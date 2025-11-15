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
    public GameObject introAnimacionGO;
    public GameObject finalPerfectoUI;
    public GameObject levelSelectScreen;

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

        // Ocultar UI al inicio
        if (caosometroUI != null)
            caosometroUI.SetActive(false);

        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);

        if (introAnimacionGO != null)
            introAnimacionGO.SetActive(false);

        if (levelSelectScreen != null)
            levelSelectScreen.SetActive(false);
    }

private void Start()
{
    StartCoroutine(ResetearMusica());
    StartCoroutine(InicializarSoundManager());
}

private IEnumerator ResetearMusica()
{
    var local = FindObjectOfType<MusicController>();
    if (MusicController.Instance != null && MusicController.Instance != local)
    {
        Destroy(MusicController.Instance.gameObject);
        yield return null; 
    }
    if (MusicController.Instance == null)
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/MusicManager"));
    }
}

private IEnumerator InicializarSoundManager()
{
    yield return null; // Esperar un frame
    var local = FindObjectOfType<SoundManager>();
    if (SoundManager.Instance != null && SoundManager.Instance != local)
    {
        Destroy(SoundManager.Instance.gameObject);
        yield return null; 
    }
    if (SoundManager.Instance == null)
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/SoundManager"));
    }
}

    public void OnPlayPressed()
    {
        if (titleScreen != null) titleScreen.SetActive(false);
        if (levelSelectScreen != null) levelSelectScreen.SetActive(true);
    }

    public void OnLevel2Selected()
    {
        if (levelSelectScreen != null) levelSelectScreen.SetActive(false);
        if (mainUI != null) mainUI.SetActive(true);
        if (caosometroUI != null) caosometroUI.SetActive(true);

        var jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            var movimiento = jugador.GetComponent<Movement>();
            if (movimiento != null)
                movimiento.SetMovimientoHabilitado(false);
        }

        GameManager.Instance.StartGame();
    }

    public void ShowGameOver(string finalText)
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void ShowFinalPerfecto()
    {
        if (caosometroUI != null)
            caosometroUI.SetActive(false);

        if (finalPerfectoUI != null)
            finalPerfectoUI.SetActive(true);
    }
}
