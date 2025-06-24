using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public bool completed = false;
    public string taskName;

    public void Interact() {
        if (completed) return;
        completed = true;
        TaskManager.Instance.RegisterTaskCompletion(taskName);
        gameObject.SetActive(false); 
    }
}
