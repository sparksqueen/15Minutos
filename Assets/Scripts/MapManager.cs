using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Arrastra aquí las imágenes de los mapas")]
    public GameObject mapaNivel1; // Mapa de la Abuela
    public GameObject mapaNivel2; // Mapa Original (Casa)
    public GameObject mapaNivel3; // Mapa del Museo

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (GameManager.Instance != null && GameManager.Instance.gameActive)
            {
                ToggleMapa();
            }
        }
    }

    void ToggleMapa()
    {
        // 1. Verificamos si ya hay algún mapa abierto para cerrarlo (Toggle)
        bool algunMapaAbierto = (mapaNivel1 != null && mapaNivel1.activeSelf) ||
                                (mapaNivel2 != null && mapaNivel2.activeSelf) ||
                                (mapaNivel3 != null && mapaNivel3.activeSelf);

        // Si hay uno abierto, cerramos todo.
        if (algunMapaAbierto)
        {
            CerrarTodosLosMapas();
        }
        else
        {
            // Si están cerrados, abrimos el que corresponde al nivel actual
            AbrirMapaDelNivelActual();
        }
    }

    void AbrirMapaDelNivelActual()
    {
        // Preguntamos al LevelManager en qué nivel estamos
        // (Asegúrate de que tu LevelManager tenga la función GetCurrentLevel, si no la tiene avísame)
        int nivelActual = 0;
        
        if (LevelManager.Instance != null)
        {
            nivelActual = LevelManager.Instance.GetCurrentLevel();
        }

        switch (nivelActual)
        {
            case 1:
                if (mapaNivel1 != null) mapaNivel1.SetActive(true);
                break;
            case 2:
                if (mapaNivel2 != null) mapaNivel2.SetActive(true);
                break;
            case 3:
                if (mapaNivel3 != null) mapaNivel3.SetActive(true);
                break;
            default:
                Debug.LogWarning("Nivel desconocido o Menú Principal. No hay mapa para mostrar.");
                break;
        }
    }

    void CerrarTodosLosMapas()
    {
        if (mapaNivel1 != null) mapaNivel1.SetActive(false);
        if (mapaNivel2 != null) mapaNivel2.SetActive(false);
        if (mapaNivel3 != null) mapaNivel3.SetActive(false);
    }
}