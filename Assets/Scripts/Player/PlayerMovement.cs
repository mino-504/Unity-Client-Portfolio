using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private PlayerInput playerInput;

    void Awake()
    {
        // 같은 GameObject(Player)에 붙은 PlayerInput을 가져옴
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // 입력 컴포넌트에서 이동 입력을 가져옴
        Vector2 input = playerInput.MoveInput;

        // 입력값 -> 방향 벡터 (2D니까 Z=0)
        Vector3 direction = new Vector3(input.x, input.y, 0f).normalized;

        // 이동 적용 (프레임 독립)
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
