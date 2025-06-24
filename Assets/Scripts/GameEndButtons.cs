using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndButtons : MonoBehaviour
{
    public string escenaDeJuego = "SampleScene";     
    public string escenaCreditos = "CreditsScene";    

  
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(escenaDeJuego);
    }


    public void IrACreditos()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(escenaCreditos);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
    #endif
    }
}
