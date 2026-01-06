using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Melee Attack Data (좌클릭)")]
    [SerializeField] private PlayerAttackData meleeData;

    [Header("Ranged Attack Data (우클릭)")]
    [SerializeField] private PlayerRangedAttackData rangedData;

    [Header("Debug")]
    [SerializeField] private bool showAttackGizmo = true;

    private float lastMeleeTime = -999f;
    private float lastRangedTime = -999f;

    private PlayerInput input;

    void Awake()
    {
        input = GetComponent<PlayerInput>();

        if (input == null)
            Debug.LogError("[PlayerCombat] PlayerInput이 없습니다.");

        if (meleeData == null)
            Debug.LogError("[PlayerCombat] MeleeData가 할당되지 않았습니다.");

        if (meleeData != null && meleeData.attackEffectPrefab == null)
            Debug.LogError("[PlayerCombat] MeleeData에 AttackEffectPrefab이 비어있습니다.");

        if (rangedData == null)
            Debug.LogError("[PlayerCombat] RangedData가 할당되지 않았습니다.");

        if (rangedData != null && rangedData.projectilePrefab == null)
            Debug.LogError("[PlayerCombat] RangedData에 Projectile Prefab이 비어있습니다.");
    }

    void Update()
    {
        if (input == null) return;

        // 좌클릭: 근접 공격
        if (meleeData != null && input.AttackHeld)
            TryMelee();

        // 우클릭: 원거리 공격
        if (rangedData != null && input.SecondaryAttackHeld)
            TryRanged();
    }

    // =====================
    // Melee
    // =====================
    void TryMelee()
    {
        if (Time.time - lastMeleeTime < meleeData.cooldown) return;
        lastMeleeTime = Time.time;

        Vector3 playerPos = transform.position;
        Vector3 mousePos = input.MouseWorldPosition;

        Vector2 dir = (mousePos - playerPos).normalized;
        if (dir.sqrMagnitude < 0.0001f) return;

        Vector3 attackPos = playerPos + (Vector3)(dir * meleeData.effectOffset);

        // 이펙트
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject fx = Instantiate(
            meleeData.attackEffectPrefab,
            attackPos,
            Quaternion.Euler(0, 0, angle)
        );
        Destroy(fx, meleeData.effectDuration);

        // 데미지 판정
        Collider2D hit = Physics2D.OverlapCircle(
            attackPos,
            meleeData.range,
            meleeData.enemyLayer
        );

        if (hit != null && hit.TryGetComponent<IDamageable>(out var dmg))
            dmg.TakeDamage(meleeData.damage);
    }

    // =====================
    // Ranged
    // =====================
    void TryRanged()
    {
        if (Time.time - lastRangedTime < rangedData.cooldown) return;
        lastRangedTime = Time.time;

        FireProjectile();
    }

    void FireProjectile()
    {
        Vector3 playerPos = transform.position;
        Vector3 mousePos = input.MouseWorldPosition;

        Vector2 dir = (mousePos - playerPos).normalized;
        if (dir.sqrMagnitude < 0.0001f) return;

        Vector3 spawnPos = playerPos + (Vector3)(dir * rangedData.spawnOffset);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle);

        GameObject go = Instantiate(rangedData.projectilePrefab, spawnPos, rot);

        if (go.TryGetComponent<Projectile>(out var proj))
        {
            proj.Init(
                dir,
                rangedData.projectileSpeed,
                rangedData.damage,
                rangedData.projectileLifetime,
                rangedData.hitLayer   // Enemy + Wall
            );
        }
        else
        {
            Debug.LogError("[PlayerCombat] Projectile 프리팹에 Projectile.cs가 없습니다.");
        }
    }

    // =====================
    // Gizmo
    // =====================
    private void OnDrawGizmosSelected()
    {
        if (!showAttackGizmo || meleeData == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeData.range);
    }
}
