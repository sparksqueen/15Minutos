using UnityEngine;

public class IntroAnimacionManager : MonoBehaviour
{
    public GameObject uiPrincipal;
    public GameObject animacionContainer;

    public void FinDeAnimacion()
    {
        if (animacionContainer != null)
            animacionContainer.SetActive(false);

        if (uiPrincipal != null)
            uiPrincipal.SetActive(true);

        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            Movement movimiento = jugador.GetComponent<Movement>();
            if (movimiento != null)
                movimiento.SetMovimientoHabilitado(true);
        }
    }
}
