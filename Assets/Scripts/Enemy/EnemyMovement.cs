using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private EnemyController enemy;

    void Awake()
    {
        enemy = GetComponent<EnemyController>();

        if (enemy == null)
            Debug.LogError("[EnemyMovement] EnemyController가 없습니다.");
    }

    void Start()
    {
        if (enemy != null && enemy.PlayerTransform == null)
            Debug.LogError("[EnemyMovement] PlayerTransform을 찾지 못했습니다. Player 태그 확인!");
    }

    // 🔹 추적 이동 (비물리)
    public void MoveTowardsPlayer()
    {
        if (enemy == null || enemy.PlayerTransform == null) return;

        Vector3 targetPos = enemy.PlayerTransform.position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );
    }

    // 🔹 Attack 상태에서 호출용 (비물리라 사실상 비어 있어도 됨)
    public void Stop()
    {
        // 비물리 이동이므로 별도 처리 필요 없음
        // (가독성 / 상태 의미용으로만 남겨둠)
    }
}
