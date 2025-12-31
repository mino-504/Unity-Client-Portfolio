using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyController enemy, EnemyStateMachine fsm)
        : base(enemy, fsm) { }

    public override void Enter()
    {
        Debug.Log("Enemy State: Idle");
    }

    public override void Tick()
    {
        float distance = Vector2.Distance(
            enemy.transform.position,
            enemy.PlayerTransform.position
        );

        // ✅ EnemyController에 있는 DetectRange 사용
        if (distance <= enemy.DetectRange)
        {
            fsm.ChangeState(enemy.ChaseState);
        }

        if (distance <= enemy.AttackRange)
            {
                fsm.ChangeState(enemy.AttackState);
                return;
            }
    }
}
