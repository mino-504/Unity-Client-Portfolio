using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;

    private Transform player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public void MoveTowardsPlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
