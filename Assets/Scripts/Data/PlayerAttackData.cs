using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Attack Data")]
public class PlayerAttackData : ScriptableObject
{
    public int damage = 1;
    public float range = 1f;
    public float cooldown = 0.3f;
    public LayerMask enemyLayer;

    public float effectOffset = 0.6f;
    public float effectDuration = 0.1f;
}
