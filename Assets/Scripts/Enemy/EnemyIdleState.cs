using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyController enemy, EnemyStateMachine fsm)
        : base(enemy, fsm) { }

    public override void Enter()
    {
        Debug.Log("Enemy State: Idle");

        // ✅ Idle 진입 시 "진짜 정지" (밀려서 생긴 velocity 제거)
        enemy.Movement.Stop();
    }

    public override void Tick()
    {
        if (enemy.PlayerTransform == null) return;

        float distance = Vector2.Distance(
            enemy.transform.position,
            enemy.PlayerTransform.position
        );

        // ✅ 가까우면 공격이 우선 (AttackRange <= DetectRange여야 자연스러움)
        if (distance <= enemy.AttackRange)
        {
            fsm.ChangeState(enemy.AttackState);
            return;
        }

        // ✅ 감지 범위면 추적
        if (distance <= enemy.DetectRange)
        {
            fsm.ChangeState(enemy.ChaseState);
            return;
        }

        // ✅ 그 외엔 Idle 유지(아무것도 안 함)
    }
}
