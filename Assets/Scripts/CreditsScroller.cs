using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroller : MonoBehaviour
{
    public RectTransform creditsText;
    public float scrollSpeed = 40f;
    public float delayBeforeReturn = 2f;
    public string mainMenuSceneName = "MainMenu";

    private float endY;
    private bool finished = false;

    void Start()
    {
        endY = Screen.height + creditsText.rect.height;
        
        FindObjectOfType<MusicController>()?.ResetPitch();
    }

    void Update()
    {
        if (finished) return;

        creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (creditsText.anchoredPosition.y >= endY)
        {
            finished = true;
            Invoke(nameof(ReturnToMenu), delayBeforeReturn);
        }
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
