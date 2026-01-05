using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Data")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float attackCooldown = 0.3f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Attack Effect")]
    [SerializeField] private GameObject attackEffect;
    [SerializeField] private float effectOffset = 0.6f;
    [SerializeField] private bool showAttackGizmo = true;

    private float lastAttackTime = -999f;
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput == null)
            Debug.LogError("[PlayerCombat] PlayerInput이 없습니다. Player에 PlayerInput 컴포넌트를 붙여주세요!");

        if (attackEffect == null)
            Debug.LogError("[PlayerCombat] AttackEffect가 할당되지 않았습니다. Inspector에서 연결하세요!");
    }

    void Update()
    {
        if (playerInput == null) return;

        HandleAttackInput();
    }

    // =========================
    // Input Handling
    // =========================
    void HandleAttackInput()
    {
        if (playerInput.AttackHeld)
        {
            TryAttack();
        }
    }

    // =========================
    // Attack Flow
    // =========================
    void TryAttack()
    {
        if (!CanAttack()) return;

        ExecuteAttack();
    }

    bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackCooldown;
    }

    void ExecuteAttack()
    {
        lastAttackTime = Time.time;

        // 1. 공격 방향 계산 (플레이어 → 마우스)
        Vector3 playerPos = transform.position;
        Vector3 mousePos = playerInput.MouseWorldPosition;

        Vector2 dir = (mousePos - playerPos).normalized;

        // 2. 공격 기준 위치 계산
        Vector3 attackPos = playerPos + (Vector3)(dir * effectOffset);

        // 3. 이펙트 처리
        PlayAttackEffect(attackPos, dir);

        // 4. 공격 판정
        ApplyDamage(attackPos);
    }

    // =========================
    // Effect
    // =========================
    void PlayAttackEffect(Vector3 position, Vector2 direction)
    {
        if (attackEffect == null) return;

        attackEffect.transform.position = position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        attackEffect.transform.rotation = Quaternion.Euler(0, 0, angle);

        StartCoroutine(ShowAttackEffect());
    }

    IEnumerator ShowAttackEffect()
    {
        attackEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackEffect.SetActive(false);
    }

    // =========================
    // Damage
    // =========================
    void ApplyDamage(Vector3 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, attackRange, enemyLayer);

        if (hit != null && hit.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);
            Debug.Log("[Player] Enemy Hit!");
        }
    }

    // =========================
    // Debug
    // =========================
    private void OnDrawGizmosSelected()
    {
        if (!showAttackGizmo) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
