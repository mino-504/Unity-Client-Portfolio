using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ✅ Inspector에서 조절할 튜닝 값(거리)
    [SerializeField] private float detectRange = 3f;
    [SerializeField] private float loseRange = 4f;

    // ✅ 상태(State)들이 읽기만 하도록 공개(Setter 없음)
    public float DetectRange => detectRange;
    public float LoseRange => loseRange;

    public Transform PlayerTransform { get; private set; }
    public EnemyMovement Movement { get; private set; }

    public EnemyIdleState IdleState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }

    private EnemyStateMachine fsm;

    void Awake()
    {
        PlayerTransform = GameObject.FindWithTag("Player").transform;
        Movement = GetComponent<EnemyMovement>();

        fsm = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, fsm);
        ChaseState = new EnemyChaseState(this, fsm);

        fsm.ChangeState(IdleState);
    }

    void Update()
    {
        fsm.Tick();
    }
}
