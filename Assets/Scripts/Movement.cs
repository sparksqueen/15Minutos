using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Movement : MonoBehaviour
{
    public float speed = 2.5f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
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
}
