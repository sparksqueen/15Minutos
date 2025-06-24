using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Movement : MonoBehaviour
{
    public float speed = 2.5f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool puedeMoverse = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!puedeMoverse)
        {
            // Asegurarse de que la animación se detenga si está bloqueado
            UpdateAnimation(Vector2.zero);
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveX, moveY).normalized;
        transform.position += (Vector3)movement * speed * Time.deltaTime;

        UpdateAnimation(movement);
    }

    void UpdateAnimation(Vector2 movement)
    {
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        bool isMoving = movement != Vector2.zero;
        animator.SetBool("IsMoving", isMoving);
    }

    // Método público para habilitar o deshabilitar movimiento
    public void SetMovimientoHabilitado(bool activo)
    {
        puedeMoverse = activo;
    }
}
