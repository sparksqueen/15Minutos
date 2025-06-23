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
    public string objetoID; // Para validación en zonas objetivo
    public InteraccionTipo tipoDeInteraccion = InteraccionTipo.PegarAlJugador;

    public Sprite spriteLimpio;
    public GameObject promptText;
    public Vector3 offset = new Vector3(0.5f, 0.5f, 0f);

    private SpriteRenderer sr;
    private bool jugadorCerca = false;
    private Transform jugador;
    private bool estaPegado = false;
    private Collider2D col;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        if (promptText != null)
            promptText.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.Space))
        {
            if (tipoDeInteraccion != InteraccionTipo.PegarAlJugador)
            {
                EjecutarInteraccion();
                if (promptText != null)
                    promptText.SetActive(false);
            }
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

            case InteraccionTipo.PegarAlJugador:
                // Esta lógica está gestionada por PlayerPickup, así que no se ejecuta desde acá.
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

            if (promptText != null && tipoDeInteraccion != InteraccionTipo.PegarAlJugador)
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
