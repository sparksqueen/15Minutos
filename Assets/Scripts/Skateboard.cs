using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skateboard : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void TriggerDisappearance()
    {
        StartCoroutine(DisappearAfterDelay(5f));
    }

    private System.Collections.IEnumerator DisappearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.enabled = false;
        if (rb2D != null) Destroy(rb2D);
    }
}
