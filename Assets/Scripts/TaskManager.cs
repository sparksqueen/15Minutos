using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    public List<string> allTasks;
    public int completedTasks = 0;

    private void Awake() {
        if (Instance == null) Instance = this;
    }

    public void EnableAllTasks() {
        completedTasks = 0;
        // habilitá todos los objetos Task
    }

    public void RegisterTaskCompletion(string taskName) {
        completedTasks++;
    }

    public int GetCompletionPercent() {
        return Mathf.FloorToInt((completedTasks / (float)allTasks.Count) * 100);
    }
}

