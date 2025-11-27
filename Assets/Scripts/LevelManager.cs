using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Nivel 1")]
    public GameObject lvl1Obj;
    public GameObject lvl1Limits;
    public Transform spawnLvl1;

    [Header("Nivel 2 (House)")]
    public GameObject houseObjects;
    public GameObject houseLimits;
    public Transform spawnLvl2;

    [Header("Nivel 3")]
    public GameObject lvl3Obj;
    public GameObject lvl3Limits;
    public Transform spawnLvl3;

    [Header("Referencias Comunes")]
    public GameObject player;
    public CaosometroManager caosometroManager;

    private int currentLevel = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // BLOQUEO DE SEGURIDAD AL INICIO DEL JUEGO
        InicializarPlayer(false); 

        if (currentLevel == 0)
        {
            DesactivarTodosLosNiveles();
        }
    }

    public void InicializarNivel(int nivel)
    {
        currentLevel = nivel;
        DesactivarTodosLosNiveles();

        Transform puntoDeSpawn = null;

        switch (nivel)
        {
            case 1:
                if (lvl1Obj != null) lvl1Obj.SetActive(true);
                if (lvl1Limits != null) lvl1Limits.SetActive(true);
                puntoDeSpawn = spawnLvl1;
                break;
            case 2:
                if (houseObjects != null) houseObjects.SetActive(true);
                if (houseLimits != null) houseLimits.SetActive(true);
                puntoDeSpawn = spawnLvl2;
                break;
            case 3:
                if (lvl3Obj != null) lvl3Obj.SetActive(true);
                if (lvl3Limits != null) lvl3Limits.SetActive(true);
                puntoDeSpawn = spawnLvl3;
                break;
        }

        // 1. Mover al jugador a su posición
        if (player != null && puntoDeSpawn != null)
        {
            player.transform.position = puntoDeSpawn.position;
            player.transform.rotation = puntoDeSpawn.rotation;
        }

        // 2. ¡IMPORTANTE! FORZAMOS EL APAGADO ANTES DE LA INTRO
        InicializarPlayer(false); 

        // 3. Iniciamos el contador de 33 segundos
        StopAllCoroutines(); // Detiene cualquier timer viejo por si acaso
        StartCoroutine(EsperarFinDeIntro());

        ReinicializarCaosometro();
    }

    // --- EL TEMPORIZADOR DE LA INTRO ---
    private IEnumerator EsperarFinDeIntro()
    {
        Debug.Log("LEVELMANAGER: Iniciando Intro de 33 segundos. Jugador debe estar QUIETO.");
        
        // REFUERZO: Nos aseguramos una vez más de que esté quieto
        InicializarPlayer(false);

        // Esperamos 33 segundos
        yield return new WaitForSeconds(33f);

        Debug.Log("LEVELMANAGER: Fin de Intro. Jugador LIBERADO.");
        
        // ¡AHORA SÍ! LO DEJAMOS MOVER
        InicializarPlayer(true);
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

    private void InicializarPlayer(bool permitirMovimiento)
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.SetActive(true);
            Movement movimiento = player.GetComponent<Movement>();
            
            if (movimiento != null)
            {
                // Activamos o desactivamos según lo que diga el código
                movimiento.SetMovimientoHabilitado(permitirMovimiento);
                
                // Si bloqueamos, frenamos la física también
                if (!permitirMovimiento)
                {
                    Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                    if (rb != null) rb.velocity = Vector2.zero;
                }
            }
        }
    }

    private void ReinicializarCaosometro()
    {
        StartCoroutine(ReinicializarCaosometroCoroutine());
    }

    private IEnumerator ReinicializarCaosometroCoroutine()
    {
        yield return null;
        if (caosometroManager == null) caosometroManager = FindObjectOfType<CaosometroManager>();
        if (caosometroManager != null) caosometroManager.Inicializar();
    }
    // --- ESTO ES LO QUE FALTABA ---
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}