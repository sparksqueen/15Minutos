using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para recargar

public class GameEndButtons : MonoBehaviour
{
    // Esta función la usas en el botón "Ir al Nivel 2" del final del Nivel 1
    public void IrAlNivel2()
    {
        Debug.Log("--- INICIANDO LIMPIEZA TOTAL PARA NIVEL 2 ---");
        Time.timeScale = 1f;

        // 1. Configuramos el UIManager para que sepa que al volver a nacer debe ir al Nivel 2
        if (UIManager.Instance != null)
        {
            UIManager.NivelPendienteDeCargar = 2;
        }

        // 2. MATAMOS AL PLAYER (El portador de la silla)
        // Esto es vital. Si el player sobrevive (DontDestroyOnLoad), la silla sobrevive.
        // Al matarlo, forzamos al juego a crear uno nuevo y limpio en la siguiente carga.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }

        // 3. MATAMOS CUALQUIER OBJETO SUELTO (La silla rebelde)
        // Buscamos cualquier cosa que sea "Interactable" o "Pickup" y la destruimos
        GameObject[] basuras = GameObject.FindGameObjectsWithTag("Pickup");
        foreach (GameObject b in basuras) Destroy(b);
        
        GameObject[] interactuables = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject i in interactuables) Destroy(i);

        // 4. EL REINICIO NUCLEAR
        // LoadScene borra todo lo que no sea "DontDestroyOnLoad" y recarga el archivo original.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Esta función la usas en el botón "Ir al Nivel 3"
    public void IrAlNivel3()
    {
        Debug.Log("--- INICIANDO LIMPIEZA TOTAL PARA NIVEL 3 ---");
        Time.timeScale = 1f;

        if (UIManager.Instance != null)
        {
            UIManager.NivelPendienteDeCargar = 3;
        }

        // Matamos al Player viejo
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) Destroy(player);

        // Reinicio Nuclear
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Tus otras funciones...
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}