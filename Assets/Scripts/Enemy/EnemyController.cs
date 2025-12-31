using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ✅ Inspector에서 조절할 튜닝 값(거리)
    [SerializeField] private float detectRange = 3f;
    [SerializeField] private float loseRange = 4f;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float attackExitRange = 1.3f;
    public float AttackExitRange => attackExitRange;
    public float DetectRange => detectRange;
    public float LoseRange => loseRange;
    public float AttackRange => attackRange;

    public Transform PlayerTransform { get; private set; }
    public EnemyMovement Movement { get; private set; }
    public EnemyAttack Attack { get; private set; }
    
    public EnemyIdleState IdleState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    private EnemyStateMachine fsm;

    void Awake()
    {
        PlayerTransform = GameObject.FindWithTag("Player").transform;
        Movement = GetComponent<EnemyMovement>();
        Attack = GetComponent<EnemyAttack>();

        // ✅ fsm 먼저 생성!
        fsm = new EnemyStateMachine();

        // ✅ 그 다음 상태 생성
        IdleState = new EnemyIdleState(this, fsm);
        ChaseState = new EnemyChaseState(this, fsm);
        AttackState = new EnemyAttackState(this, fsm);

        fsm.ChangeState(IdleState);
    }

    void Update()
    {
        fsm.Tick();
    }
}
