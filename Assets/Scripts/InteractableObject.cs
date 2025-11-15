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
    public string objetoID;
    public InteraccionTipo tipoDeInteraccion = InteraccionTipo.PegarAlJugador;
    public Sprite spriteLimpio;
    public Vector3 offset = new Vector3(0.5f, 0.5f, 0f);
    public AudioClip brushSoundClip;

    private SpriteRenderer sr;
    private bool jugadorCerca = false;
    private Transform jugador;
    private Collider2D col;
    private static GameObject promptText;

    private bool yaContado = false;
    private static AudioClip brushSound; 

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        if (promptText == null)
            promptText = GameObject.FindGameObjectWithTag("Prompt");

        if (promptText != null)
            promptText.SetActive(false);

        // Cargar el sonido de cepillo solo una vez (estático)
        if (brushSound == null)
        {
            // Primero intentar usar el clip asignado desde el Inspector
            if (brushSoundClip != null)
            {
                brushSound = brushSoundClip;
            }
            else
            {
                // Si no está asignado, intentar cargarlo desde Resources
                brushSound = Resources.Load<AudioClip>("Audio/brush-83215");
            }
        }
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.Space))
        {
            if (tipoDeInteraccion != InteraccionTipo.PegarAlJugador)
            {
                EjecutarInteraccion();
                OcultarPrompt();
            }
        }
    }

    void EjecutarInteraccion()
    {
        if (!yaContado)
        {
            FindObjectOfType<CaosometroManager>()?.ObjetoOrdenado();
            yaContado = true;
        }

        // Reproducir sonido de limpieza
        ReproducirSonidoLimpieza();

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
                break;
        }

        gameObject.tag = "Untagged";
    }

    void ReproducirSonidoLimpieza()
    {
        // Intentar usar SoundManager si está disponible
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBrushSound();
        }
        else
        {
            Debug.LogWarning("InteractableObject: SoundManager.Instance es null, usando fallback");
            // Fallback: usar el clip estático o el del inspector
            AudioClip clipToPlay = brushSound != null ? brushSound : brushSoundClip;
            
            if (clipToPlay != null)
            {
                // Obtener o crear un AudioSource para reproducir el sonido
                AudioSource audioSource = GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
                audioSource.PlayOneShot(clipToPlay);
                Debug.Log("InteractableObject: Reproduciendo sonido de limpieza (fallback)");
            }
            else
            {
                Debug.LogWarning("InteractableObject: No hay clip de sonido disponible");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            jugador = other.transform;

            if (promptText != null && tipoDeInteraccion != InteraccionTipo.PegarAlJugador)
            {
                promptText.SetActive(true);
                var tmp = promptText.GetComponentInChildren<TMP_Text>();
                if (tmp != null)
                {
                    switch (tipoDeInteraccion)
                    {
                        case InteraccionTipo.CambiarSprite:
                            tmp.text = "Presione espacio para limpiar";
                            break;
                        case InteraccionTipo.Desactivar:
                        case InteraccionTipo.Destruir:
                            tmp.text = "Presione espacio para eliminar";
                            break;
                    }
                }
            }
            else if (promptText != null && tipoDeInteraccion == InteraccionTipo.PegarAlJugador)
            {
                promptText.SetActive(true);
                var tmp = promptText.GetComponentInChildren<TMP_Text>();
                if (tmp != null)
                    tmp.text = "Mantenga G para mover o M para ver el mapa";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            OcultarPrompt();
        }
    }

    private void OcultarPrompt()
    {
        if (promptText != null)
            promptText.SetActive(false);
    }
}
