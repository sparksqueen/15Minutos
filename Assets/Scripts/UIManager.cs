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
    }
    public void OnPlayPressed()
    {
        titleScreen.SetActive(false);
        mainUI.SetActive(true);
        GameManager.Instance.StartGame();
    }

    public void ShowGameOver(string finalText)
    {
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponentInChildren<TextMeshProUGUI>().text = finalText;

        // Elegir imagen según final
        if (finalText.Contains("perfecto"))
            gameOverBackground.sprite = finalPerfecto;
        else if (finalText.Contains("desordenado"))
            gameOverBackground.sprite = finalDesordenado;
        else
            gameOverBackground.sprite = finalCatastrofico;
    }

// void Start()
// {
//     ShowGameOver("Final catastrofico");
// }
}

