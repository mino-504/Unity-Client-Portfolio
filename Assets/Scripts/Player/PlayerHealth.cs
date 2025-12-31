using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp = 10;
    public int CurrentHp { get; private set; }

    void Awake()
    {
        CurrentHp = maxHp;
        Debug.Log($"[Player] HP Init: {CurrentHp}/{maxHp}");
    }

    public void TakeDamage(int damage)
    {
        CurrentHp = Mathf.Max(0, CurrentHp - damage);
        Debug.Log($"[Player] Took {damage} damage. HP: {CurrentHp}/{maxHp}");

        if (CurrentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("[Player] Died");
        // Phase 4-2에서는 일단 비활성화로 처리
        gameObject.SetActive(false);
    }
}
