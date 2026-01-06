using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Ranged Attack Data")]
public class PlayerRangedAttackData : ScriptableObject
{
    [Header("Combat")]
    public int damage = 1;
    public float cooldown = 0.4f;
    public LayerMask enemyLayer;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 12f;
    public float projectileLifetime = 2f;

    [Header("Hit Layer")]
    public LayerMask hitLayer;   // ★ Enemy + Wall

    [Header("Spawn")]
    public float spawnOffset = 0.6f;
}
