using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyController enemy, EnemyStateMachine fsm)
        : base(enemy, fsm) { }

    public override void Enter()
    {
        Debug.Log("Enemy State: Attack");
    }

    public override void Tick()
    {
        // 공격 중에는 이동 멈추고 싶으면
        enemy.Movement.Stop();

        // 공격 실행 (쿨타임은 EnemyAttack이 관리)
        enemy.Attack.TryAttack();

        float distance = Vector2.Distance(enemy.transform.position, enemy.PlayerTransform.position);

        // AttackRange가 아니라 "ExitRange"로 빠져나가면 상태 떨림이 줄어듦
        if (distance > enemy.AttackExitRange)
        {
            fsm.ChangeState(enemy.ChaseState);
        }
    }
}
