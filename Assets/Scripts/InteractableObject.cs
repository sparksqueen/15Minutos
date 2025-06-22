using UnityEngine;
using TMPro;

// 🟡 Esto va fuera de la clase
public enum InteraccionTipo
{
    CambiarSprite,
    Desactivar,
    Destruir
}

public class InteractableObject : MonoBehaviour
{
    public InteraccionTipo tipoDeInteraccion = InteraccionTipo.CambiarSprite;

    public Sprite spriteLimpio;
    public GameObject promptText;

    private SpriteRenderer sr;
    private bool jugadorCerca = false;
    private bool fueUsado = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (promptText != null)
            promptText.SetActive(false);
    }

    void Update()
    {
        if (fueUsado) return;

        if (jugadorCerca && Input.GetKeyDown(KeyCode.Space))
        {
            EjecutarInteraccion();
            fueUsado = true;

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
            if (!fueUsado && promptText != null)
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