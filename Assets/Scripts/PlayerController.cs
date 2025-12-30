using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Inspector에서 속도를 조절할 수 있게 노출 (하드코딩 방지)
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {
        // [1] 입력(Input) 수집: 이동은 값(h, v)으로 들어온다
        float h = Input.GetAxisRaw("Horizontal"); // A/D, ←/→
        float v = Input.GetAxisRaw("Vertical");   // W/S, ↑/↓

        // [2] 입력값 → 방향 벡터로 변환 (대각선 속도 보정을 위해 normalized)
        Vector3 direction = new Vector3(h, v, 0f).normalized;

        // [3] 이동 적용: Time.deltaTime으로 프레임 의존성 제거
        transform.position += direction * moveSpeed * Time.deltaTime;

        // [4] 마우스 Screen 좌표 → World 좌표로 변환
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // [5] 플레이어 위치 기준으로 마우스 방향 벡터 계산
        Vector2 lookDir = mouseWorld - transform.position;

        // [6] Atan2로 각도 계산 후 Z축 회전(2D)
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
