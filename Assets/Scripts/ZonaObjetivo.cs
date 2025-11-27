using UnityEngine;

public class ZonaObjetivo : MonoBehaviour
{
    public Transform posicionFinal;
    
    [Header("Ajuste de Posicionamiento")]
    [Tooltip("Qué parte del sprite debe coincidir con el PuntoDeEncaje")]
    public PuntoReferencia puntoReferencia = PuntoReferencia.PivoteDelSprite;
    
    public enum PuntoReferencia
    {
        PivoteDelSprite,        // Usa el pivote del sprite (comportamiento por defecto)
        Centro,                 // Centro del sprite
        EsquinaInferiorIzquierda,
        EsquinaInferiorDerecha,
        EsquinaSuperiorIzquierda,
        EsquinaSuperiorDerecha,
        CentroInferior,
        CentroSuperior,
        CentroIzquierda,
        CentroDerecha
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent != null) return;

        string nombreObjeto = other.gameObject.name;
        string nombreZona = gameObject.name;

        if (nombreObjeto == nombreZona)
        {
            // --- INICIO DEL CAMBIO ---
            
            // 1. Obtenemos la posición del destino (PuntoDeEncaje)
            Vector3 posDestino = posicionFinal.position;
            
            // 2. Calculamos el offset basado en el punto de referencia seleccionado
            Vector3 offsetPorPivote = Vector3.zero;
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
            
            // Si usamos el pivote del sprite directamente, no calculamos offset
            if (puntoReferencia != PuntoReferencia.PivoteDelSprite && sr != null && sr.sprite != null)
            {
                Sprite sprite = sr.sprite;
                
                // Pivote normalizado del sprite (0-1)
                Vector2 pivotNormalizado = sprite.pivot / sprite.rect.size;
                
                // Tamaño del sprite en unidades del mundo
                float pixelsPerUnit = sprite.pixelsPerUnit;
                Vector2 tamañoSprite = sprite.rect.size / pixelsPerUnit;
                
                // Punto de referencia deseado (normalizado 0-1)
                Vector2 puntoDeseado = GetPuntoReferenciaNormalizado(puntoReferencia);
                
                // Calculamos el offset: diferencia entre el pivote actual y el punto deseado
                // Si el pivote está en (0.5, 0.5) y queremos el centro (0.5, 0.5), offset = 0
                // Si el pivote está en (0.5, 0) y queremos el centro (0.5, 0.5), necesitamos mover hacia arriba
                offsetPorPivote.x = (puntoDeseado.x - pivotNormalizado.x) * tamañoSprite.x;
                offsetPorPivote.y = (puntoDeseado.y - pivotNormalizado.y) * tamañoSprite.y;
            }
            
            // 3. Posicionamos el objeto ajustando por el offset del pivote
            Vector3 posSegura = posDestino - offsetPorPivote;
            
            // 4. Le ponemos una Z negativa manual (ej: -5) para asegurar que flote 
            // por delante de cualquier pared o fondo.
            posSegura.z = -5f; 
            
            // 5. Aplicamos la posición corregida
            other.transform.position = posSegura;

            // 6. "Fuerza Bruta" visual: Buscamos el SpriteRenderer y le subimos la prioridad
            if (sr != null)
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
    
    private Vector2 GetPuntoReferenciaNormalizado(PuntoReferencia punto)
    {
        switch (punto)
        {
            case PuntoReferencia.PivoteDelSprite:
                // Este caso se maneja en el código principal (no se calcula offset)
                return new Vector2(0.5f, 0.5f); // No se usa, pero necesitamos retornar algo
            case PuntoReferencia.Centro:
                return new Vector2(0.5f, 0.5f);
            case PuntoReferencia.EsquinaInferiorIzquierda:
                return new Vector2(0f, 0f);
            case PuntoReferencia.EsquinaInferiorDerecha:
                return new Vector2(1f, 0f);
            case PuntoReferencia.EsquinaSuperiorIzquierda:
                return new Vector2(0f, 1f);
            case PuntoReferencia.EsquinaSuperiorDerecha:
                return new Vector2(1f, 1f);
            case PuntoReferencia.CentroInferior:
                return new Vector2(0.5f, 0f);
            case PuntoReferencia.CentroSuperior:
                return new Vector2(0.5f, 1f);
            case PuntoReferencia.CentroIzquierda:
                return new Vector2(0f, 0.5f);
            case PuntoReferencia.CentroDerecha:
                return new Vector2(1f, 0.5f);
            default:
                return new Vector2(0.5f, 0.5f);
        }
    }
}
