using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform holdPoint;
    private GameObject heldObject;

    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            if (heldObject == null) TryPickup();
        }
        else
        {
            if (heldObject != null) DropObject();
        }
    }

    void TryPickup()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var col in colliders)
        {
            // CORRECCIÓN: Volvemos a la lógica estricta.
            // SOLO agarramos si tiene el tag "Pickup".
            // Borré la parte que decía "|| col.CompareTag("Interactable")" porque eso rompía todo.
            
            if (col.CompareTag("Pickup")) 
            {
                heldObject = col.gameObject;
                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;

                if (heldObject.TryGetComponent(out Rigidbody2D rb))
                    rb.simulated = false;

                if (heldObject.TryGetComponent(out Collider2D collider))
                    collider.enabled = false;

                break;
            }
        }
    }

    void DropObject()
    {
        if (heldObject == null) return;

        // 1. Reactivamos físicas PRIMERO (Vital para que el trigger de la zona lo detecte)
        if (heldObject.TryGetComponent(out Collider2D collider)) collider.enabled = true;
        if (heldObject.TryGetComponent(out Rigidbody2D rb)) rb.simulated = true;

        // 2. DETECCIÓN DE ZONAS (Usamos los tags de tu foto)
        bool tocandoZona = false;
        
        Collider2D[] toques = Physics2D.OverlapCircleAll(heldObject.transform.position, 0.2f);
        foreach (var toque in toques)
        {
            // AQUÍ ESTÁ LA CLAVE: Buscamos tus tags específicos
            if (toque.CompareTag("Zona") || toque.CompareTag("ZonaObjetivo")) 
            {
                tocandoZona = true;
                Debug.Log("Soltando sobre zona: " + toque.name); // Para verificar en consola
                break;
            }
        }

        // 3. DECISIÓN DEL PADRE
        if (tocandoZona)
        {
            // SI TOCA ZONA: Lo dejamos sin padre (null).
            // Tu script de acomodar detectará el trigger y se lo robará.
            heldObject.transform.SetParent(null);
        }
        else
        {
            // SI CAE AL PISO: Lo guardamos en el Nivel.
            Transform padreDelNivel = null;

            if (LevelManager.Instance != null)
            {
                int nivelActual = LevelManager.Instance.GetCurrentLevel();
                switch (nivelActual)
                {
                    case 1:
                        if (LevelManager.Instance.lvl1Obj != null) 
                            padreDelNivel = LevelManager.Instance.lvl1Obj.transform;
                        break;
                    case 2:
                        if (LevelManager.Instance.houseObjects != null) 
                            padreDelNivel = LevelManager.Instance.houseObjects.transform;
                        break;
                    case 3:
                        if (LevelManager.Instance.lvl3Obj != null) 
                            padreDelNivel = LevelManager.Instance.lvl3Obj.transform;
                        break;
                }
            }
            heldObject.transform.SetParent(padreDelNivel);
        }

        heldObject = null;
    }

    public bool IsHolding(GameObject obj) => heldObject == obj;
    public void ForzarSoltar() => DropObject();
}