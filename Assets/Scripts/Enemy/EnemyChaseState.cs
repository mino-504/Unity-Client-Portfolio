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
        float distance = Vector2.Distance(enemy.transform.position, enemy.PlayerTransform.position);

        // ✅ 1) 놓치면 Idle (가장 먼저)
        if (distance > enemy.LoseRange)
        {
            fsm.ChangeState(enemy.IdleState);
            return;
        }

        // ✅ 2) 공격 범위면 Attack
        if (distance <= enemy.AttackRange)
        {
            fsm.ChangeState(enemy.AttackState);
            return;
        }

        // ✅ 3) 그 외에는 추적 이동
        enemy.Movement.MoveTowardsPlayer();
    }
}
