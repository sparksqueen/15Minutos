using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 1.5f;
    public LayerMask interactableLayer; // Asegurate de poner la patineta en ese layer

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D obj = Physics2D.OverlapCircle(transform.position, interactRange, interactableLayer);
            if (obj != null && obj.CompareTag("Skateboard"))
            {
                obj.GetComponent<Skateboard>().TriggerDisappearance();
            }
        }
    }
}
