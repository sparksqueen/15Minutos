using UnityEngine;
using UnityEngine.UI;

public class CaosometroManager : MonoBehaviour
{
    public Slider caosometroSlider;
    private int totalObjetos;
    private int objetosRestantes;

    void Start()
    {
        // Contar automáticamente los objetos con tag "Interactable" y "Pickup"
        int interactables = GameObject.FindGameObjectsWithTag("Interactable").Length;
        int pickups = GameObject.FindGameObjectsWithTag("Pickup").Length;
        totalObjetos = interactables + pickups;

        objetosRestantes = totalObjetos;

        // Configurar correctamente el slider
        if (caosometroSlider != null)
        {
            caosometroSlider.minValue = 0f;
            caosometroSlider.maxValue = 1f;
            caosometroSlider.wholeNumbers = false;
        }

        ActualizarBarra();
    }

    public void ReducirCaos()
    {
        objetosRestantes = Mathf.Max(0, objetosRestantes - 1);
        ActualizarBarra();

        if (objetosRestantes == 0)
        {
            UIManager.Instance.ShowFinalPerfecto();

        }
    }

    public void ObjetoOrdenado()
    {
        ReducirCaos();
    }

    void ActualizarBarra()
    {
        if (caosometroSlider == null || totalObjetos == 0)
        {
            return;
        }

        float porcentaje = (float)objetosRestantes / totalObjetos;
        caosometroSlider.value = porcentaje;
    }
}
