using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool gameActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        gameActive = true;
        TimerManager.Instance.StartTimer();
        TaskManager.Instance.EnableAllTasks();
    }

public void EndGame(bool ordenPerfecto = false)
{
    gameActive = false;

    if (ordenPerfecto)
    {
        SceneManager.LoadScene("FinalPerfectoScene");
        return;
    }

    int percent = TaskManager.Instance.GetCompletionPercent();

    if (percent >= 90)
        SceneManager.LoadScene("FinalPerfectoScene");
    else
        SceneManager.LoadScene("GameOverScene");
}

}
