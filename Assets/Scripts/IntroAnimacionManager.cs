using UnityEngine;

public class IntroAnimacionManager : MonoBehaviour
{
    public GameObject uiPrincipal;       // UI del juego
    public GameObject animacionContainer; // Objeto con las imágenes y animación

    public void FinDeAnimacion()
    {
        if (animacionContainer != null)
            animacionContainer.SetActive(false); // Oculta la animación

        if (uiPrincipal != null)
            uiPrincipal.SetActive(true); // Activa la UI del juego

        // ✅ Rehabilitar movimiento del jugador
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            Movement movimiento = jugador.GetComponent<Movement>();
            if (movimiento != null)
                movimiento.SetMovimientoHabilitado(true);
        }
    }
}
