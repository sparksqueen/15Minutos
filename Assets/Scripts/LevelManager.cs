using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Nivel 1")]
    public GameObject lvl1Obj;
    public GameObject lvl1Limits;

    [Header("Nivel 2 (House)")]
    public GameObject houseObjects;
    public GameObject houseLimits;

    [Header("Nivel 3")]
    public GameObject lvl3Obj;
    public GameObject lvl3Limits;

    [Header("Referencias Comunes")]
    public GameObject player;
    public CaosometroManager caosometroManager;

    private int currentLevel = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Al inicio, desactivar todos los niveles
        // Solo si no se ha inicializado ningún nivel todavía
        if (currentLevel == 0)
        {
            DesactivarTodosLosNiveles();
        }
    }

    public void InicializarNivel(int nivel)
    {
        currentLevel = nivel;

        // Desactivar todos los niveles primero
        DesactivarTodosLosNiveles();

        // Activar el nivel correspondiente
        switch (nivel)
        {
            case 1:
                if (lvl1Obj != null) lvl1Obj.SetActive(true);
                if (lvl1Limits != null) lvl1Limits.SetActive(true);
                break;

            case 2:
                if (houseObjects != null) houseObjects.SetActive(true);
                if (houseLimits != null) houseLimits.SetActive(true);
                break;

            case 3:
                if (lvl3Obj != null) lvl3Obj.SetActive(true);
                if (lvl3Limits != null) lvl3Limits.SetActive(true);
                break;

            default:
                Debug.LogWarning($"Nivel {nivel} no reconocido");
                return;
        }

        // Inicializar el player
        InicializarPlayer();

        // Reinicializar el caosometro para contar solo los objetos del nivel activo
        ReinicializarCaosometro();
    }

    private void DesactivarTodosLosNiveles()
    {
        if (lvl1Obj != null) lvl1Obj.SetActive(false);
        if (lvl1Limits != null) lvl1Limits.SetActive(false);

        if (houseObjects != null) houseObjects.SetActive(false);
        if (houseLimits != null) houseLimits.SetActive(false);

        if (lvl3Obj != null) lvl3Obj.SetActive(false);
        if (lvl3Limits != null) lvl3Limits.SetActive(false);
    }

    private void InicializarPlayer()
    {
        // Si no hay referencia directa, buscar por tag
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player != null)
        {
            player.SetActive(true);

            // Asegurarse de que el movimiento esté deshabilitado inicialmente
            Movement movimiento = player.GetComponent<Movement>();
            if (movimiento != null)
            {
                movimiento.SetMovimientoHabilitado(false);
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el Player en la escena");
        }
    }

    private void ReinicializarCaosometro()
    {
        // Esperar un frame para asegurar que todos los objetos estén activos
        StartCoroutine(ReinicializarCaosometroCoroutine());
    }

    private System.Collections.IEnumerator ReinicializarCaosometroCoroutine()
    {
        // Esperar un frame para que Unity procese los cambios de activación
        yield return null;

        // Si no hay referencia directa, buscar en la escena
        if (caosometroManager == null)
        {
            caosometroManager = FindObjectOfType<CaosometroManager>();
        }

        if (caosometroManager != null)
        {
            // Reinicializar el caosometro para contar solo los objetos del nivel activo
            caosometroManager.Inicializar();
        }
        else
        {
            Debug.LogWarning("No se encontró CaosometroManager en la escena");
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}

