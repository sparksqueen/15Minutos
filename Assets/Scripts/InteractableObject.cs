using UnityEngine;
using TMPro;

public enum InteraccionTipo
{
    CambiarSprite,
    Desactivar,
    Destruir, 
    PegarAlJugador
}

public class InteractableObject : MonoBehaviour
{
    public InteraccionTipo tipoDeInteraccion = InteraccionTipo.CambiarSprite;

    public Sprite spriteLimpio;
    public GameObject promptText;

    private SpriteRenderer sr;
    private bool jugadorCerca = false;
    private Transform jugador;
    private PlayerPickup pickupScript;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (promptText != null)
            promptText.SetActive(false);

        pickupScript = FindObjectOfType<PlayerPickup>();
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.Space))
        {
            if (pickupScript != null && pickupScript.EstáSosteniendo())
            {
                Debug.Log("No se puede interactuar mientras sostenés un objeto");
                return;
            }

            EjecutarInteraccion();

            if (promptText != null)
                promptText.SetActive(false);
        }
    }

    void EjecutarInteraccion()
    {
        switch (tipoDeInteraccion)
        {
            case InteraccionTipo.CambiarSprite:
                if (spriteLimpio != null && sr != null)
                    sr.sprite = spriteLimpio;
                break;

            case InteraccionTipo.Desactivar:
                gameObject.SetActive(false);
                break;

            case InteraccionTipo.Destruir:
                Destroy(gameObject);
                break;
        }

        gameObject.tag = "Untagged";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            jugador = other.transform;

            if (promptText != null)
                promptText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;

            if (promptText != null)
                promptText.SetActive(false);
        }
    }
}
