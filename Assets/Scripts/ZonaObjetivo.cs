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
            other.transform.position = posicionFinal.position;

            if (other.TryGetComponent(out Rigidbody2D rb))
                rb.simulated = false;

            if (other.TryGetComponent(out Collider2D col))
                col.enabled = false;

            if (other.tag == "Pickup") // solo cuenta si era "Pickup"
            {
                // ✔️ Previene doble conteo
                other.tag = "Untagged";

                // ✔️ Marca como entregado
                if (!other.TryGetComponent<YaEntregado>(out _))
                {
                    other.gameObject.AddComponent<YaEntregado>();
                    FindObjectOfType<CaosometroManager>()?.ObjetoOrdenado();
                }
            }
        }
    }
}
