using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private EnemyController enemy;
    private Rigidbody2D rb;

    void Awake()
    {
        enemy = GetComponent<EnemyController>();
        rb = GetComponent<Rigidbody2D>();

        if (enemy == null)
            Debug.LogError("[EnemyMovement] EnemyController가 없습니다.");

        // 추천(실수 방지)
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    public void MoveTowardsPlayer()
    {
        if (enemy == null || enemy.PlayerTransform == null) return;

        Vector2 currentPos = rb.position;
        Vector2 targetPos = enemy.PlayerTransform.position;

        Vector2 dir = (targetPos - currentPos).normalized;
        Vector2 nextPos = currentPos + dir * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextPos);
    }

    public void Stop()
    {
        if (rb == null) return;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }


}
