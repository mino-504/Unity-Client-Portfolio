using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp = 5;
    public int CurrentHp { get; private set; }

    void Awake()
    {
        CurrentHp = maxHp;
        Debug.Log($"[Enemy] HP Init: {CurrentHp}/{maxHp}");
    }

    public void TakeDamage(int damage)
    {
        CurrentHp = Mathf.Max(0, CurrentHp - damage);
        Debug.Log($"[Enemy] Took {damage} damage. HP: {CurrentHp}/{maxHp}");

        if (CurrentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
