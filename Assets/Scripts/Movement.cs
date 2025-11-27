using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Movement : MonoBehaviour
{
    public float speed = 2.5f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // CAMBIO CLAVE: "SerializeField private"
    // Esto hace que puedas ver la casilla en Unity para debug, 
    // PERO impide que otros scripts la toquen directamente.
    // Además, la iniciamos en FALSE.
    [SerializeField] private bool puedeMoverse = false;

    void Awake()
    {
        // Forzamos el apagado al nacer, por si acaso.
        puedeMoverse = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Si está apagado, cortamos aquí.
        if (puedeMoverse == false)
        {
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
        if (animator != null)
        {
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
            bool isMoving = movement != Vector2.zero;
            animator.SetBool("IsMoving", isMoving);
        }
    }

    // ESTA ES LA ÚNICA PUERTA DE ENTRADA
    public void SetMovimientoHabilitado(bool activo)
    {
        // Este mensaje nos dirá en la consola QUIÉN activó el movimiento y CUÁNDO
        if (activo == true)
        {
            Debug.Log("¡ALERTA! Alguien activó el movimiento. Hora: " + Time.time);
            // Si quieres ver un rastro avanzado (opcional):
            // Debug.Log(System.Environment.StackTrace); 
        }

        puedeMoverse = activo;
    }
}