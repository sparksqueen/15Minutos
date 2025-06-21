using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEvaluator : MonoBehaviour
{
    public static FinalEvaluator Instance; // ✅ Esto es lo que te falta

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

    public void Evaluate()
    {
        int percent = TaskManager.Instance.GetCompletionPercent();

        if (percent >= 90) UIManager.Instance.ShowGameOver("Final Perfecto");
        else if (percent >= 50) UIManager.Instance.ShowGameOver("Final Meh");
        else UIManager.Instance.ShowGameOver("Final Catastrófico");
    }
}
