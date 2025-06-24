using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUIController : MonoBehaviour
{
    public void JugarDeNuevo()
    {
        // Suponiendo que tu escena principal se llama "MainScene"
        SceneManager.LoadScene("SampleScene");
    }

    public void IrACreditos()
    {
        // Suponiendo que la escena de créditos se llama "CreditosScene"
        SceneManager.LoadScene("CreditosScene");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
