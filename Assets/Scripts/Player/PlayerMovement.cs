using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        // 추천(실수 방지)
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (playerInput == null) return;

        Vector2 input = playerInput.MoveInput.normalized;
        Vector2 nextPos = rb.position + input * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextPos);
    }
}
