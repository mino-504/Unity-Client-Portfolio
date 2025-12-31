public class EnemyStateMachine
{
    private EnemyState currentState;

    public void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return; // ✅ 같은 상태면 무시

        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void Tick()
    {
        currentState?.Tick();
    }
}
