using UnityEngine;
using System.Collections; // Necesario para usar Corrutinas (tiempos)

public class IntroAnimacionManager : MonoBehaviour
{
    [Header("Configuración")]
    public float tiempoDuracion = 4.0f; // Cuánto dura la intro en segundos
    public GameObject panelVisual; // (Opcional) Arrastra aquí lo que quieras mostrar

    private Movement playerMovement;

    void OnEnable() // Se activa cuando arranca el nivel
    {
        // 1. Buscamos al jugador y lo bloqueamos
        playerMovement = FindFirstObjectByType<Movement>(); 
        
        if (playerMovement != null)
        {
            playerMovement.SetMovimientoHabilitado(false); // ¡Quieto!
        }

        // 2. Activamos lo visual (si hay algo asignado)
        if (panelVisual != null) panelVisual.SetActive(true);

        // 3. Iniciamos el conteo regresivo
        StartCoroutine(EsperarYLiberar());
    }

    IEnumerator EsperarYLiberar()
    {
        // Esperamos los segundos que dijiste (ej: 4 segundos)
        yield return new WaitForSeconds(tiempoDuracion);

        // ¡YA PASÓ EL TIEMPO!
        FinalizarIntro();
    }

    void FinalizarIntro()
    {
        // Liberamos al jugador
        if (playerMovement != null)
        {
            playerMovement.SetMovimientoHabilitado(true); // ¡Corre libre!
        }

        // Apagamos el objeto de la intro para que no moleste
        gameObject.SetActive(false);
    }
}