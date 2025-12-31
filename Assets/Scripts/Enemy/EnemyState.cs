using UnityEngine;

public abstract class EnemyState
{
    protected EnemyController enemy;
    protected EnemyStateMachine fsm;

    protected EnemyState(EnemyController enemy, EnemyStateMachine fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    // 상태에 들어갈 때 1번
    public virtual void Enter() { }

    // 상태 유지 중 매 프레임
    public virtual void Tick() { }

    // 상태에서 나갈 때 1번
    public virtual void Exit() { }
}
