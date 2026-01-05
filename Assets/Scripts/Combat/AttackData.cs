using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attack Data")]
public class AttackData : ScriptableObject
{
    [Header("Stats")]
    public int damage = 1;
    public float range = 1.0f;
    public float cooldown = 0.3f;

    [Header("Effect")]
    public GameObject attackEffect;
    public float effectOffset = 0.6f;
}
