using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private PlayerInput playerInput;

    void Awake()
    {
        // 같은 GameObject(Player)에 붙은 PlayerInput 참조
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // PlayerInput이 계산해 둔 마우스 월드 좌표 가져오기
        Vector3 mouseWorld = playerInput.MouseWorldPosition;

        // 플레이어 -> 마우스 방향 벡터
        Vector2 lookDir = mouseWorld - transform.position;

        // 방향 벡터 -> 각도(degree)
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        // 2D 회전 (Z축만)
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
