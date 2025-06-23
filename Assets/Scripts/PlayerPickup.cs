using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform holdPoint;
    private GameObject heldObject;

    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            if (heldObject == null)
                TryPickup();
        }
        else
        {
            if (heldObject != null)
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

                if (heldObject.TryGetComponent(out Collider2D collider))
                    collider.enabled = false;

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

        if (heldObject.TryGetComponent(out Collider2D collider))
            collider.enabled = true;

        heldObject = null;
    }

    public bool IsHolding(GameObject obj)
    {
        return heldObject == obj;
    }

    public void ForzarSoltar()
    {
        DropObject();
    }
}
