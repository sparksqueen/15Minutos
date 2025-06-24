using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //este lo tiene comentado
        TimerManager.Instance.StartTimer();
        TaskManager.Instance.EnableAllTasks();
    }

    public void EndGame()
    {
        gameActive = false;
        FinalEvaluator.Instance.Evaluate();
    }

    // esto es para testear 
    //     void Start()
    // {
    //     StartGame();
    // }
}
