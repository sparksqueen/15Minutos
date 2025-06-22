using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform holdPoint;
    private GameObject heldObject;
    private bool isHolding = false;

    void Update()
    {
        // Si estás sosteniendo algo con G, bloquear SPACE
        if (isHolding && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("No se puede limpiar mientras sostenés un objeto.");
            return;
        }

        // Si presionás G, agarrar
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!isHolding)
                TryPickup();
        }

        // Si soltás G, soltar
        if (Input.GetKeyUp(KeyCode.G))
        {
            if (isHolding)
                DropObject();
        }
    }

    void TryPickup()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var col in colliders)
        {
            if (col.CompareTag("Pickup"))
            {
                heldObject = col.gameObject;
                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;

                if (heldObject.TryGetComponent(out Rigidbody2D rb))
                    rb.simulated = false;

                if (heldObject.TryGetComponent(out Collider2D col2D))
                    col2D.enabled = false;

                isHolding = true;
                break;
            }
        }
    }

    void DropObject()
    {
        if (heldObject == null) return;

        heldObject.transform.SetParent(null);

        if (heldObject.TryGetComponent(out Rigidbody2D rb))
            rb.simulated = true;

        if (heldObject.TryGetComponent(out Collider2D col2D))
            col2D.enabled = true;

        heldObject = null;
        isHolding = false;
    }

    public bool EstáSosteniendo()
    {
        return isHolding;
    }
}
