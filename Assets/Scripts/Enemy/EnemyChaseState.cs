using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyController enemy, EnemyStateMachine fsm)
        : base(enemy, fsm) { }

    public override void Enter()
    {
        Debug.Log("Enemy State: Chase");
    }

    public override void Tick()
    {
        // ✅ 추적 이동
        enemy.Movement.MoveTowardsPlayer();

        float distance = Vector2.Distance(
            enemy.transform.position,
            enemy.PlayerTransform.position
        );

        // ✅ EnemyController에 있는 LoseRange 사용
        if (distance > enemy.LoseRange)
        {
            fsm.ChangeState(enemy.IdleState);
        }
    }
}
