using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    private Vector2 dir;
    private float speed;
    private int damage;
    private float lifeTime;
    private LayerMask hitLayer;

    private float spawnTime;

    public void Init(
        Vector2 direction,
        float speed,
        int damage,
        float lifeTime,
        LayerMask hitLayer
    )
    {
        this.dir = direction.normalized;
        this.speed = speed;
        this.damage = damage;
        this.lifeTime = lifeTime;
        this.hitLayer = hitLayer;

        spawnTime = Time.time;
    }

    void Update()
    {
        transform.position += (Vector3)(dir * speed * Time.deltaTime);

        if (Time.time - spawnTime >= lifeTime)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 1️⃣ 맞을 레이어인지 확인 (Enemy or Wall)
        if (((1 << other.gameObject.layer) & hitLayer.value) == 0)
            return;

        // 2️⃣ Enemy면 데미지
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);
        }

        // 3️⃣ 벽이든 적이든, 맞으면 투사체 삭제
        Destroy(gameObject);
    }
}
