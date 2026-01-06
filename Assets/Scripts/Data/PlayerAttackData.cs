using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Attack Data")]
public class PlayerAttackData : ScriptableObject
{
    [Header("Combat")]
    public int damage = 1;
    public float range = 1f;
    public float cooldown = 0.3f;
    public LayerMask enemyLayer;

    [Header("Effect")]
    public GameObject attackEffectPrefab;   // ← ★ 추가됨
    public float effectOffset = 0.6f;
    public float effectDuration = 0.1f;
}
