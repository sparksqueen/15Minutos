using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Sprite spriteLimpio;
    private SpriteRenderer sr;
    private bool jugadorCerca = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.Space))
        {
            sr.sprite = spriteLimpio;
            gameObject.tag = "Untagged"; // Ya no es interactuable
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = false;
    }
}
