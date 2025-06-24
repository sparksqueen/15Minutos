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

  
    if (UIManager.Instance != null && UIManager.Instance.introAnimacionGO != null)
    {
        GameObject animGO = UIManager.Instance.introAnimacionGO;
        animGO.SetActive(true);

        Animator animator = animGO.GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false; 
            animator.enabled = true;
            animator.Play(0, -1, 0f); 
        }
    }

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
