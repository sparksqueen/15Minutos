using UnityEngine;

public class ZonaObjetivo : MonoBehaviour
{
    public Transform posicionFinal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent != null) return;

        string nombreObjeto = other.gameObject.name;
        string nombreZona = gameObject.name;

        if (nombreObjeto == nombreZona)
        {
            // --- INICIO DEL CAMBIO ---
            
            // 1. Obtenemos la posición del destino, pero ignoramos su Z
            Vector3 posSegura = posicionFinal.position;
            
            // 2. Le ponemos una Z negativa manual (ej: -5) para asegurar que flote 
            // por delante de cualquier pared o fondo.
            posSegura.z = -5f; 
            
            // 3. Aplicamos la posición corregida
            other.transform.position = posSegura;

            // 4. "Fuerza Bruta" visual: Buscamos el SpriteRenderer y le subimos la prioridad
            if (other.TryGetComponent(out SpriteRenderer sr))
            {
                sr.sortingOrder = 50; // Número alto para que se dibuje encima de todo
                sr.maskInteraction = SpriteMaskInteraction.None; // Evita que mascaras lo oculten
            }

            // --- FIN DEL CAMBIO ---

            if (other.TryGetComponent(out Rigidbody2D rb))
                rb.simulated = false;

            if (other.TryGetComponent(out Collider2D col))
                col.enabled = false;

            if (other.tag == "Pickup") // solo cuenta si era "Pickup"
            {
                other.tag = "Untagged";

                if (!other.TryGetComponent<YaEntregado>(out _))
                {
                    other.gameObject.AddComponent<YaEntregado>();
                    FindObjectOfType<CaosometroManager>()?.ObjetoOrdenado();
                }
            }
        }
    }
}
