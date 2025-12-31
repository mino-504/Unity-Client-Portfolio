using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // 이동 입력 값 (외부에서는 읽기만 가능)
    public Vector2 MoveInput { get; private set; }

    // 마우스의 월드 좌표
    public Vector3 MouseWorldPosition { get; private set; }

    void Update()
    {
        // 1. 키보드 이동 입력 수집
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        MoveInput = new Vector2(h, v);

        // 2. 마우스 위치 수집 (Screen → World)
        MouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
