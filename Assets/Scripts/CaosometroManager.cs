using UnityEngine;
using UnityEngine.UI;

public class CaosometroManager : MonoBehaviour
{
    public Slider caosometroSlider;
    private int totalObjetos;
    private int objetosRestantes;

    void Start()
    {
        Inicializar();
    }

    public void Inicializar()
    {
        // Contar automáticamente los objetos con tag "Interactable" y "Pickup" que estén activos
        GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

        int countInteractables = 0;
        int countPickups = 0;

        // Contar solo los objetos activos
        foreach (GameObject obj in interactables)
        {
            if (obj != null && obj.activeInHierarchy)
            {
                countInteractables++;
            }
        }

        foreach (GameObject obj in pickups)
        {
            if (obj != null && obj.activeInHierarchy)
            {
                countPickups++;
            }
        }

        totalObjetos = countInteractables + countPickups;
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
