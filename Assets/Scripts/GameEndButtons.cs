using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndButtons : MonoBehaviour
{
    public string escenaDeJuego = "SampleScene";      // Nombre de tu escena principal
    public string escenaCreditos = "CreditsScene";    // Nombre de la escena de créditos

    // Llamado por el botón "REINICIAR"
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f; // Por si se pausó el juego
        SceneManager.LoadScene(escenaDeJuego);
    }

    // Llamado por el botón "CREDITOS"
    public void IrACreditos()
    {
        Time.timeScale = 1f; // Por si se pausó el juego
        SceneManager.LoadScene(escenaCreditos);
    }

    // Llamado por el botón "X"
    public void SalirDelJuego()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Para que funcione en el editor
#endif
    }
}
