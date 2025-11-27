using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{

    public static int NivelPendienteDeCargar = 0;
    public static UIManager Instance;

    [Header("--- CONFIGURACIÓN MANUAL DEL NIVEL ---")]
    [Tooltip("IMPORTANTE: Pon 1 si es Abuela, 2 si es Original, 3 si es Museo")]
    public int ID_Nivel_Actual = 1; // <--- ESTA ES LA VARIABLE NUEVA CLAVE

    [Header("UI General")]
    public GameObject mainUI;
    public GameObject titleScreen;
    public GameObject caosometroUI;
    public GameObject introAnimacionGO;
    public GameObject levelSelectScreen;

    [Header("--- NIVEL 1 (TUTORIAL ABUELA) ---")]
    public GameObject gameOverNivel1;
    public GameObject finalPerfectoNivel1;

    [Header("--- NIVEL 2 (ORIGINAL) ---")]
    public GameObject gameOverNivel2;
    public GameObject finalPerfectoNivel2;

    [Header("--- NIVEL 3 (MUSEO) ---")]
    public GameObject gameOverNivel3;       
    public GameObject finalPerfectoNivel3;  
    
    // Esta variable interna tomará el valor de la pública al iniciar
    private int nivelActualJugando = 0;

    private void Awake()
    {
        // Singleton simple: Si ya hay uno, me mato yo. Si no, soy yo.
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return; // Cortamos aquí para que no ejecute lo de abajo
        }

        Instance = this;

        // --- BORRA ESTA LÍNEA SI LA TIENES ---
        // DontDestroyOnLoad(gameObject);  <-- ¡ESTA ES LA CULPABLE!
        // -------------------------------------

        // Al quitar esa línea, cuando recargues la escena, el UIManager viejo morirá,
        // nacerá uno nuevo, ejecutará Start() y leerá que tiene que ir al Nivel 2.

        nivelActualJugando = ID_Nivel_Actual;
        
        // Ocultar TODO al inicio
        OcultarTodasLasUI();
    }

    private void Start()
    {
        StartCoroutine(ResetearMusica());
        StartCoroutine(InicializarSoundManager());

        // --- ESTO ES LO NUEVO ---
        // Si hay un nivel pendiente (porque venimos de ganar un nivel), lo cargamos directo
        if (NivelPendienteDeCargar != 0)
        {
            int nivel = NivelPendienteDeCargar;
            NivelPendienteDeCargar = 0; // Limpiamos la variable para la próxima
            InicializarNivel(nivel);
        }
        // ------------------------
    }

    // --- CORRUTINAS DE AUDIO (IGUAL QUE ANTES) ---
    private IEnumerator ResetearMusica()
    {
        var local = FindObjectOfType<MusicController>();
        if (MusicController.Instance != null && MusicController.Instance != local)
        {
            Destroy(MusicController.Instance.gameObject);
            yield return null; 
        }
        if (MusicController.Instance == null)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/MusicManager"));
        }
    }

    private IEnumerator InicializarSoundManager()
    {
        yield return null; 
        var local = FindObjectOfType<SoundManager>();
        if (SoundManager.Instance != null && SoundManager.Instance != local)
        {
            Destroy(SoundManager.Instance.gameObject);
            yield return null; 
        }
        if (SoundManager.Instance == null)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/SoundManager"));
        }
    }
    
    public void OnPlayPressed()
    {
        if (titleScreen != null) titleScreen.SetActive(false);
        if (levelSelectScreen != null) levelSelectScreen.SetActive(true);
    }

    // --- SELECCIÓN DE NIVELES ---
    public void OnLevel1Selected() 
    { 
        OcultarTodasLasUI(); // <--- ESTO LIMPIA EL CARTEL VIEJO
        InicializarNivel(1); 
    }

    public void OnLevel2Selected() 
    { 
        OcultarTodasLasUI(); // <--- ESTO LIMPIA EL CARTEL VIEJO
        InicializarNivel(2); 
    }

    public void OnLevel3Selected() 
    { 
        OcultarTodasLasUI(); // <--- ESTO LIMPIA EL CARTEL VIEJO
        InicializarNivel(3); 
    }
    private void InicializarNivel(int nivel)
    {
        nivelActualJugando = nivel;

        // APAGAR PANTALLAS QUE MOLESTAN
        if (levelSelectScreen != null) levelSelectScreen.SetActive(false);
        
        // --- NUEVO: APAGAR EL MENU PRINCIPAL ---
        if (titleScreen != null) titleScreen.SetActive(false); 
        // ---------------------------------------

        if (mainUI != null) mainUI.SetActive(true);
        if (caosometroUI != null) caosometroUI.SetActive(true);

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.InicializarNivel(nivel);
        }

        if (GameManager.Instance != null) GameManager.Instance.StartGame();
    }

    public void ShowGameOver(string finalText)
    {
        Time.timeScale = 0f;

        // ACTIVAR PANTALLA SEGÚN EL NIVEL
        switch (nivelActualJugando)
        {
            case 1: // Abuela
                if (gameOverNivel1 != null) gameOverNivel1.SetActive(true);
                break;
            case 2: // Original
                if (gameOverNivel2 != null) gameOverNivel2.SetActive(true);
                break;
            case 3: // Museo
                if (gameOverNivel3 != null) gameOverNivel3.SetActive(true);
                break;
            default:
                // Si no sabe qué nivel es, intentamos con el 2 por defecto
                if (gameOverNivel2 != null) gameOverNivel2.SetActive(true);
                break;
        }
    }

    public void ShowFinalPerfecto()
    {
        if (caosometroUI != null) caosometroUI.SetActive(false);

        // ACTIVAR PANTALLA SEGÚN EL NIVEL
        switch (nivelActualJugando)
        {
            case 1: // Abuela
                Debug.Log("¡Activando Final Nivel 1!");
                if (finalPerfectoNivel1 != null) finalPerfectoNivel1.SetActive(true);
                else Debug.LogError("Falta asignar 'finalPerfectoNivel1' en el Inspector");
                break;
            case 2: // Original
                if (finalPerfectoNivel2 != null) finalPerfectoNivel2.SetActive(true);
                break;
            case 3: // Museo
                if (finalPerfectoNivel3 != null) finalPerfectoNivel3.SetActive(true);
                break;
            default:
                Debug.LogWarning("Nivel desconocido (" + nivelActualJugando + "), usando default.");
                if (finalPerfectoNivel2 != null) finalPerfectoNivel2.SetActive(true);
                break;
        }
    }

    public void OcultarTodasLasUI()
    {
        if (caosometroUI != null) caosometroUI.SetActive(false);
        if (introAnimacionGO != null) introAnimacionGO.SetActive(false);
        if (levelSelectScreen != null) levelSelectScreen.SetActive(false);
        
        if (gameOverNivel1 != null) gameOverNivel1.SetActive(false);
        if (finalPerfectoNivel1 != null) finalPerfectoNivel1.SetActive(false);
        
        if (gameOverNivel2 != null) gameOverNivel2.SetActive(false);
        if (finalPerfectoNivel2 != null) finalPerfectoNivel2.SetActive(false);
        
        if (gameOverNivel3 != null) gameOverNivel3.SetActive(false);
        if (finalPerfectoNivel3 != null) finalPerfectoNivel3.SetActive(false);
    }
}