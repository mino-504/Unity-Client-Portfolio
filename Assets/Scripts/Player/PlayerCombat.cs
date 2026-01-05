using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Data (ScriptableObject)")]
    [SerializeField] private PlayerAttackData attackData;

    [Header("Attack Effect")]
    [SerializeField] private GameObject attackEffect;
    [SerializeField] private bool showAttackGizmo = true;

    private float lastAttackTime = -999f;
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput == null)
            Debug.LogError("[PlayerCombat] PlayerInput이 없습니다. Player에 PlayerInput 컴포넌트를 붙여주세요!");

        if (attackData == null)
            Debug.LogError("[PlayerCombat] AttackData가 할당되지 않았습니다. Inspector에서 PlayerAttackData 에셋을 연결하세요!");

        if (attackEffect == null)
            Debug.LogError("[PlayerCombat] AttackEffect가 할당되지 않았습니다. Inspector에서 연결하세요!");
    }

    void Update()
    {
        if (playerInput == null || attackData == null) return;
        HandleAttackInput();
    }

    void HandleAttackInput()
    {
        if (playerInput.AttackHeld)
            TryAttack();
    }

    void TryAttack()
    {
        if (!CanAttack()) return;
        ExecuteAttack();
    }

    bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackData.cooldown;
    }

    void ExecuteAttack()
    {
        lastAttackTime = Time.time;

        Vector3 playerPos = transform.position;
        Vector3 mousePos = playerInput.MouseWorldPosition;

        Vector2 dir = (mousePos - playerPos).normalized;

        Vector3 attackPos = playerPos + (Vector3)(dir * attackData.effectOffset);

        PlayAttackEffect(attackPos, dir);
        ApplyDamage(attackPos);
    }

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
        yield return new WaitForSeconds(attackData.effectDuration);
        attackEffect.SetActive(false);
    }

    void ApplyDamage(Vector3 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, attackData.range, attackData.enemyLayer);

        if (hit != null && hit.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(attackData.damage);
            Debug.Log("[Player] Enemy Hit!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!showAttackGizmo || attackData == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackData.range);
    }
}
