using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackCooldown = 0.5f;

    [Header("Hit Check")]
    [SerializeField] private Transform attackPoint;     // 공격 기준점 (없으면 자기 위치)
    [SerializeField] private float attackRadius = 1.0f;  // 실제 판정 반경
    [SerializeField] private LayerMask targetLayer;      // Player 레이어만

    private float lastAttackTime = -999f;

    // AttackState에서 호출
    public bool TryAttack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return false;

        Vector3 center = attackPoint != null ? attackPoint.position : transform.position;

        Collider2D hit = Physics2D.OverlapCircle(center, attackRadius, targetLayer);

        // 🔍 디버그 로그 (여기!)
        Debug.Log($"[EnemyAttack] hit={(hit ? hit.name : "null")} center={center} r={attackRadius}");

        if (hit == null) return false;

        if (hit.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);
            lastAttackTime = Time.time;
            Debug.Log("[Enemy] Attack Hit");
            return true;
        }

        return false;
    }


    private void OnDrawGizmosSelected()
    {
        Vector3 center = attackPoint != null ? attackPoint.position : transform.position;
        Gizmos.DrawWireSphere(center, attackRadius);
    }
}

