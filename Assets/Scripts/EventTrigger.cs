using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public static EventTrigger Instance;

    private void Awake() {
        if (Instance == null) Instance = this;
    }

    public void TriggerEvent(int eventNumber) {
        switch (eventNumber) {
            case 1:
                Debug.Log("Evento 1: Llamado del ex");
                // Mostrar UI, pausar, ofrecer opciones
                break;
            case 2:
                Debug.Log("Evento 2: Vecina chusma");
                break;
        }
    }
}
